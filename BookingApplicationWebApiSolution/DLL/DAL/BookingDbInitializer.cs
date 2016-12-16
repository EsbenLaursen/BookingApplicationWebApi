﻿using DLL.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DAL
{
    public class BookingDbInitializer : DropCreateDatabaseAlways<BookingDbContext>
    {
        protected override void Seed(BookingDbContext context)
        {
            List<FootCare> Footcares = new List<FootCare>();
            List<Customer> Customers = new List<Customer>();
            List<Booking> Bookings = new List<Booking>();
            List<Image> Images = new List<Image>();
            List<Room> Rooms = new List<Room>();
            List<Review> Reviews = new List<Review>();

            Image i1 = new Image() { ImageId = 1, Path = "http://res.cloudinary.com/emildall/image/upload/v1480933038/picture5.jpg" };
            Image i2 = new Image() { ImageId = 2, Path = "http://res.cloudinary.com/emildall/image/upload/v1480933038/picture5.jpg" };
            Image i3 = new Image() { ImageId = 3, Path = "http://res.cloudinary.com/emildall/image/upload/v1480933038/picture5.jpg" };
            Image i4 = new Image() { ImageId = 4, Path = "http://res.cloudinary.com/emildall/image/upload/v1480933038/picture5.jpg" };
            Image i5 = new Image() { ImageId = 5, Path = "http://res.cloudinary.com/emildall/image/upload/v1480933038/picture5.jpg" };
            Image i10 = new Image(){ ImageId = 6, Path = "http://res.cloudinary.com/emildall/image/upload/v1480933038/picture5.jpg" };
            Image i6 = new Image() { ImageId = 7, Path = "http://res.cloudinary.com/emildall/image/upload/v1480933038/picture5.jpg" };
            Image i7 = new Image() { ImageId = 8, Path = "http://res.cloudinary.com/emildall/image/upload/v1480933038/picture5.jpg" };
            Image i8 = new Image() { ImageId = 9, Path = "http://res.cloudinary.com/emildall/image/upload/v1480933038/picture5.jpg" };
            Image i9 = new Image() { ImageId = 10, Path= "http://res.cloudinary.com/emildall/image/upload/v1480933038/picture5.jpg" };
            Images.Add(i1);
            Images.Add(i2);
            Images.Add(i3);
            Images.Add(i4);
            Images.Add(i5);
            Images.Add(i6);
            Images.Add(i7);
            Images.Add(i8);
            Images.Add(i9);
            Images.Add(i10);



            FootCare f1 = new FootCare() {Id = 1, Name = "Massage1", Price = 99, Description = "no massage"};
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

            Admin a = new Admin() { Username = "mor" , Password = "lol123"};
            context.Admins.Add(a);

            Room r1 = new Room() { Id = 1, Description = "Beutiful room", Name="Living room", Persons=3, Price=500};
            Room r2 = new Room() { Id = 2, Description = "Gross room", Name = "Bathroom", Persons = 1, Price = 600};
            Room r3 = new Room() { Id = 3, Description = "putirovroom", Name = "Bed room", Persons = 3, Price = 700 };
            Rooms.Add(r1);
            Rooms.Add(r2);
            Rooms.Add(r3);


            Booking b1 = new Booking() { Id = 1, Breakfast = true, Customer = c1, EndDate = DateTime.Now.AddDays(2), StartDate = DateTime.Now.AddDays(1), Room = new List<Room>{ r1, r2, r3 } };
            Booking b2 = new Booking() { Id = 2, Breakfast = true, Customer = c5, EndDate = DateTime.Now.AddDays(8), StartDate = DateTime.Now.AddDays(5), Room = new List<Room> { r1, r2, r3 } };
            Booking b3 = new Booking() { Id = 3, Breakfast = false, Customer = c2, EndDate = DateTime.Now.AddDays(23), StartDate = DateTime.Now.AddDays(20), Room = new List<Room> { r1, r2, r3 } };
            Booking b4 = new Booking() { Id = 4, Breakfast = true, Customer = c3, EndDate = DateTime.Now.AddDays(55), StartDate = DateTime.Now.AddDays(50), Room = new List<Room> { r1, r2, r3 } };
            Booking b5 = new Booking() { Id = 5, Breakfast = false, Customer = c5, EndDate = DateTime.Now.AddDays(30), StartDate = DateTime.Now.AddDays(28), Room = new List<Room> { r3, r2, r1 } };
            Bookings.Add(b1);
            Bookings.Add(b2);
            Bookings.Add(b3);
            Bookings.Add(b4);
            Bookings.Add(b5);

            Review re1 = new Review() { Id = 1, Description = "Nice pets", Name = "Patrick", Rating = 5 };
            Review re2 = new Review() { Id = 2, Description = "Nice pets", Name = "Patrick", Rating = 4 };
            Review re3 = new Review() { Id = 3, Description = "Nice pets", Name = "Patrick", Rating = 3 };
            Review re4 = new Review() { Id = 4, Description = "Nice pets", Name = "Patrick", Rating = 2 };
            Reviews.Add(re1);
            Reviews.Add(re2);
            Reviews.Add(re3);
            Reviews.Add(re4);

            foreach (var Review in Reviews)
            {
                context.Reviews.Add(Review);
            }

            foreach (var Image in Images)   
            {
                context.Images.Add(Image);
            }

            foreach (var Footcare in Footcares)
            {
                context.FootCares.Add(Footcare);
            }

            foreach (var Booking in Bookings)
            {
                context.Bookings.Add(Booking);
            }
            foreach (var Room in Rooms)
            {
                context.Rooms.Add(Room);
            }
            foreach (var c in Customers)
            {
                context.Customers.Add(c);
            }

            context.SaveChanges();



        }
    }
}
