using BookingApplicationWebApi;
using DLL.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApplicationWebApi;

namespace DLL.DAL
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext() : base("BookingAppDB")
        {
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<BookingDbContext>(new BookingDbInitializer());

        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FootCare> FootCares { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure CustomerId as PK for Booking
            modelBuilder.Entity<Booking>()
                .HasKey(e => e.CustomerId);

            // Configure Customer as FK for Booking
            modelBuilder.Entity<Customer>()
                .HasRequired(s => s.Booking)
                .WithRequiredPrincipal(ad => ad.Customer);


            //Configure 1-many relation between Room and Booking
            modelBuilder.Entity<Room>().HasRequired<Booking>(s => s.Booking).WithMany(o => o.Room);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
