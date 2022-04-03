#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BerrasBio.Data;
using BerrasBio.Models;
using System.Dynamic;
using NPOI.SS.Formula.Functions;


namespace BerrasBio.Controllers
{
    public class ChooseMovieController : Controller
    {
        private readonly DataContext _context;

        public ChooseMovieController(DataContext context)
        {
            _context = context;
        }

        //GET: ChooseMovie

        public async Task<IActionResult> Index() // Få ut Film lista
        {
            return View(await _context.Movies.ToListAsync());
        }

        public IActionResult GetMovie(int id) // Hitta filmerna
        {
            var movie = _context.Movies.Find(id);
            return Ok(id);
        }



        [HttpGet]
        public IActionResult Booking(int id) // Skriva ut Filmernas information.
        {
            var join = (from ci in _context.CinemaAuditoriums
                        join s in _context.ScheduledMovies on ci.Id equals s.AuditoriumId
                        join m in _context.Movies on s.MovieId equals m.Id

                        where m.Id == id && DateTime.Now < s.Start || DateTime.Now >= s.Start && DateTime.Now <= s.End // Skriver INTE ut filmer som är gårdagens datum.
                        orderby s.Start ascending
          
                        select new Booking
                        {
                            ScheduleId = s.Id,
                            MovieId = m.Title,
                            MovieStart = (DateTime)s.Start,
                            MovieEnd = (DateTime)s.End,
                            SeatsLeft = s.SeatsLeft,
                            AuditoriumName = ci.Name

                        });
                        
            return View(join);

        }

        [HttpGet]
        public IActionResult SelectSeat(int id) // Boka Tid för filmen kunden har valt
        {
            var join = (from ci in _context.CinemaAuditoriums
                        join s in _context.ScheduledMovies on ci.Id equals s.AuditoriumId
                        join m in _context.Movies on s.MovieId equals m.Id

                        where s.Id == id
                        select new Booking
                        {
                            ScheduleId = s.Id,
                            SeatsLeft = s.SeatsLeft,
                            MovieStart = (DateTime)s.Start


                        });

            foreach (var item in join) // Loopar ut om det finns sittplatser.
            {

                if(item.SeatsLeft == 0)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewData["ScheduleId"] = id;

            return View(join);

        }

        [HttpPost]
        public IActionResult SelectSeat() // Post. 
        {
            string mail = HttpContext.Request.Form["Mail"];
            string stringNumberOfSeat = HttpContext.Request.Form["NumberOfSeats"];
            string stringScheduleId = HttpContext.Request.Form["ScheduleId"];

            var scheduleId = Convert.ToInt32(stringScheduleId);
            int numberOfSeat = Convert.ToInt32(stringNumberOfSeat);

            int customerId = AddCustomer(mail);
            int reservationId = AddReservation(scheduleId, customerId, numberOfSeat);
            UpdateSeats(scheduleId, numberOfSeat);


            return RedirectToAction("Confirmation", new {id = reservationId});

        }


        public void UpdateSeats(int scheduleId, int numberOfSeats) // Updaterar sittplatserna så att rätt sittplatser skrivs ut. 
        {
            var seatsLeft = _context.ScheduledMovies.Find(scheduleId);
            
            if(seatsLeft != null)
            {
                seatsLeft.SeatsLeft = seatsLeft.SeatsLeft - numberOfSeats;
            }

            _context.SaveChanges();
        }


        
        public IActionResult Confirmation(int id) // Bokningsbekräftels.
        {
            ViewData["confirmation"] = (from ci in _context.CinemaAuditoriums
                                        join s in _context.ScheduledMovies on ci.Id equals s.AuditoriumId
                                        join m in _context.Movies on s.MovieId equals m.Id
                                        join r in _context.Reservations on s.Id equals r.ScheduledMovieId
                                        join c in _context.Customers on r.CustomerId equals c.Id

                                        where r.Id == id

                                        select new ConfirmationModel
                                        {
                                            Mail = c.Mail,
                                            ReservationId = r.Id,
                                            NumberOfSeats = r.NumberOfSeats,
                                            Movie = m.Title,
                                            AuditoriumId = ci.Name,
                                            Start = (DateTime)s.Start

                                        });


            return View(ViewData["confirmation"]);
        }

        public int AddReservation(int scheduleId, int customerId, int numberOfSeat) // Ger en reservation.
        {
            var reservation = new Reservation
            {
                ScheduledMovieId = scheduleId,
                CustomerId = customerId,
                NumberOfSeats = numberOfSeat,
            };

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return reservation.Id;
        }

        public int AddCustomer(string mail) // Lägger till kunden i boknings systemet. 
        {
            var customer = _context.Customers.FirstOrDefault(m => m.Mail == mail);
            
            if(customer == null)
            {
                customer = new Customer
                {
                    Mail = mail
                };

                _context.Customers.Add(customer);
                _context.SaveChanges();
            }



            return customer.Id;
        }


    }

}
