using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using PlaceBooking.Models;

namespace PlaceBooking.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.SetCommandTimeout(TimeSpan.FromSeconds(5));

        }

        public DbSet<Booking> DbBookings { get; set; }
        public DbSet<LocalUse> DbLocalUses { get; set; }
        public DbSet<Local> Local { get; set; }
        public DbSet<Person> Person { get; set; }


    }
}
