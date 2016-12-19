using DLL.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
            List<Room> Rooms = new List<Room>();





            FootCare f1 = new FootCare() { Id = 1, Name = "Massage1", Price = 99, Description = "no massage" };
            FootCare f2 = new FootCare() { Id = 2, Name = "Massage2", Price = 120, Description = "ok mssage" };
            FootCare f3 = new FootCare() { Id = 3, Name = "Massage3", Price = 130, Description = "betta massage" };
            FootCare f4 = new FootCare() { Id = 4, Name = "Massage4", Price = 199, Description = "topdollar" };
            FootCare f5 = new FootCare() { Id = 5, Name = "Massage5", Price = 300, Description = "Lovely massage" };
            FootCare f6 = new FootCare() { Id = 6, Name = "Massage6", Price = 1000, Description = "Great massage" };
            Footcares.Add(f1);
            Footcares.Add(f2);
            Footcares.Add(f3);
            Footcares.Add(f4);
            Footcares.Add(f5);
            Footcares.Add(f6);




            Room r1 = new Room() { Id = 1, Description = "Beautifull 3 persons room", Name = "Room 1", Persons = 3, Price = 500 };
            Room r2 = new Room() { Id = 2, Description = "1 person room with toilet and bath", Name = "Appartment 1 ", Persons = 1, Price = 600 };
            Room r3 = new Room() { Id = 3, Description = "3 person room with beds", Name = "Room 2", Persons = 3, Price = 700 };
            Rooms.Add(r1);
            Rooms.Add(r2);
            Rooms.Add(r3);




            foreach (var Footcare in Footcares)
            {
                context.FootCares.Add(Footcare);
            }

            foreach (var Room in Rooms)
            {
                context.Rooms.Add(Room);
            }


            context.SaveChanges();



        }
    }
}
