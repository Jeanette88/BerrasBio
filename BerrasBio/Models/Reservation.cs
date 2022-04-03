using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BerrasBio.Models
{
    public class Reservation
    {

        // Databas
        public int Id { get; set; }
    
        public int? ScheduledMovieId { get; set; }

        [Display(Name = "Kund ID")]
        public int CustomerId { get; set; }

        [Display(Name = "Antal Platser")]
        public int NumberOfSeats { get; set; }

        [Display(Name = "Film/Tid ID")]
        public virtual ScheduledMovie? ScheduledMovie { get; set; }

    }
}
