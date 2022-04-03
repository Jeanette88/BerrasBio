using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BerrasBio.Models
{
    public class Movie
    {
        // Databas
        public int Id { get; set; }
        
        [Display(Name = "Titel")]
        public string Title { get; set; }

        [Display(Name = "Information")]
        public string Description { get; set; }

        [Display(Name = "Minuter")]
        public int? Minutes { get; set; }

        [Display(Name = "Åldersgräns")]
        public int? AgeLimit { get; set; }

        [Display(Name = "Publiceringsår")]
        public int? PublishedYear { get; set; }

        [Display(Name = "Pris")]
        public decimal? Price { get; set; }



    }
}
