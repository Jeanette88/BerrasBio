namespace BerrasBio.Models
{
    public class ConfirmationModel
    {
        // Join
        public int ReservationId { get; set; }
        public int NumberOfSeats { get; set; }
        public string Movie { get; set; }
        public string AuditoriumId { get; set; }
        public DateTime Start { get; set; }
        public string Mail{ get; set; }

    }
}
