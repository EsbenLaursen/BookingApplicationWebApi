using DLL.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DAL
{
    public class BookingDbInitializer : DropCreateDatabaseIfModelChanges<BookingDbContext>
    {
        protected override void Seed(BookingDbContext context)
        {
            List<FootCare> Footcares = new List<FootCare>();
            List<Customer> Customers = new List<Customer>();
            List<Booking> Bookings = new List<Booking>();
            List<Image> Images = new List<Image>();
            List<Room> Rooms = new List<Room>();


            Customer c1 = new Customer() { Id = 1, Firstname = "Kenny", Lastname = "kühl", Email = "Kuhlefar@gmail.com", PhoneNr = "329573402" };
            Customer c2 = new Customer() { Id = 2, Firstname = "Anders", Lastname = "Rictra", Email = "AGMAM@gmail.com", PhoneNr = "43562362" };
            Customer c3 = new Customer() { Id = 3, Firstname = "Henrik", Lastname = "Hanse", Email = "mus@gmail.com", PhoneNr = "43512735" };
            Customer c4 = new Customer() { Id = 4, Firstname = "Kran", Lastname = "easy e", Email = "g@gmail.com", PhoneNr = "974129356" };
            Customer c5 = new Customer() { Id = 5, Firstname = "Emilio", Lastname = "vahl", Email = "popfar@gmail.com", PhoneNr = "9375385436" };
            Customer c6 = new Customer() { Id = 6, Firstname = "Sandy", Lastname = "Esko", Email = "Babe@gmail.com", PhoneNr = "8532846477" };
            Customers.Add(c1);
            Customers.Add(c2);
            Customers.Add(c3);
            Customers.Add(c4);
            Customers.Add(c5);
            Customers.Add(c6);



            

          

        }
    }
}
