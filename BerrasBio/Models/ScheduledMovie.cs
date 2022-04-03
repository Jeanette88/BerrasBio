using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BerrasBio.Models
{
    public class ScheduledMovie
    {
        // Databas
        public int Id { get; set; }
        public int? MovieId { get; set; }
        public int? AuditoriumId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int SeatsLeft { get; set; }

        public virtual CinemaAuditorium? Auditorium { get; set; }
        public  virtual Movie? Movie { get; set; }
    }
}
