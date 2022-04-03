using System.ComponentModel.DataAnnotations;

namespace BerrasBio.Models;

public class Booking
{
    // Join
    public int Id { get; set; }

    [Display(Name = "Titel")]
    public string MovieId { get; set; }

    [Display(Name = "Start")]
    public DateTime MovieStart { get; set; }

    public int SeatId { get; set; }

    [Display(Name = "Slut")]
    public DateTime MovieEnd { get; set; }
    public int Amount { get; set; }

    [Display(Name = "Platser kvar")]
    public int SeatsLeft { get; set; }


    [Display(Name = "Film ID")]
    public int ScheduleId { get; set; }

    [Display(Name = "Salong")]
    public string AuditoriumName { get; set; }

    public string Mail { get; set; }

}
