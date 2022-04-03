using BerrasBio.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BerrasBio.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

          
        }

        public DbSet<CinemaAuditorium> CinemaAuditoriums { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ScheduledMovie> ScheduledMovies { get; set; }
        public DbSet<BerrasBio.Models.Booking> Booking { get; set; }

        public DbSet<Customer> Customers { get; set;  }
    }
}
