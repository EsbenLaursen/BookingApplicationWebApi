using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DLL;
using DLL.DAL.Entities;
using DLL.DAL.Managers;
using DLL.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestS
{
    [TestClass]
    public class CheckRoomsAvailabilityTest
    {
        private static IList<Booking> bookings = new List<Booking>();
        private static IList<Room> rooms = new List<Room>();


        [TestInitialize]
        public void TestInitializer()
        {
            bookings.Clear();
            rooms.Clear();
        }

        /// <summary>
        /// Check if you can book in the past
        /// </summary>
        [TestMethod]
        public void CheckForPastDateParametersTest()
        {
            var rooms = new List<Room>();
            var books = new List<Booking>();

            var startdate = DateTime.Now;
            var enddate = DateTime.Now;

            var past = DateTime.Now.AddDays(-1);

            Room r = new Room() { Id = 1, Name = "Carlo" };
            Room rr = new Room() { Id = 2, Name = "Morten" };

            rooms.Add(r);
            rooms.Add(rr);

            Booking b = new Booking() { Id = 1, Room = new List<Room> { r }, EndDate = enddate, StartDate = startdate };
            books.Add(b);

            var ava = new CheckRoomAvailability(rooms, books);

            var availeblerooms = ava.Check(past, past);

            Assert.AreEqual(0, availeblerooms.Count);

        }
        /// <summary>
        /// Checks if there are any available rooms if there are 0 rooms
        /// </summary>
        [TestMethod]
        public void CheckForWithoutRoomTest()
        {
            var rooms = new List<Room>();
            var books = new List<Booking>();

            var startdate = DateTime.Now;
            var enddate = DateTime.Now;


            Booking b = new Booking() { Id = 1, EndDate = enddate, StartDate = startdate };
            books.Add(b);

            var ava = new CheckRoomAvailability(rooms, books);


            var list = ava.Check(startdate, enddate);

            Assert.AreEqual(0, list.Count);

        }
        /// <summary>
        /// Checks if there are no bookings all rooms can be booked
        /// </summary>
        [TestMethod]
        public void CheckForWithoutBookingsTest()
        {
            var rooms = new List<Room>();
            var books = new List<Booking>();

            var startdate = DateTime.Now;
            var enddate = DateTime.Now.AddDays(1);

            var past = DateTime.Now.AddDays(-1);

            Room r = new Room() { Id = 1, Name = "Carlo" };
            Room rr = new Room() { Id = 2, Name = "Morten" };

            rooms.Add(r);
            rooms.Add(rr);


            var ava = new CheckRoomAvailability(rooms, books);

            var availeblerooms = ava.Check(startdate, enddate);

            Assert.AreEqual(2, availeblerooms.Count);

        }
        /// <summary>
        /// Checks if a room cannot be booked if theres is already a booking on the date
        /// </summary>
        [TestMethod]
        public void CheckForOverlappingDatesTest()
        {
            var rooms = new List<Room>();
            var books = new List<Booking>();

            var startdate = DateTime.Now;
            var enddate = DateTime.Now.AddDays(1);

            Room r = new Room() { Id = 1, Name = "Carlo" };
            Room rr = new Room() { Id = 2, Name = "Morten" };

            rooms.Add(r);
            rooms.Add(rr);

            Booking b = new Booking() { Id = 1, Room = new List<Room> { r }, StartDate = startdate, EndDate = enddate };
            r.Bookings = new List<Booking> { b };
            books.Add(b);

            var ava = new CheckRoomAvailability(rooms, books);

            var availeblerooms = ava.Check(startdate, enddate);

            Assert.AreEqual(1, availeblerooms.Count);
            Assert.AreEqual(rr.Id, availeblerooms[0].Id);

        }


        /// <summary>
        /// Checks if a room can be booked eventhough  there are bookings on other dates
        /// </summary>
        [TestMethod]
        public void CheckForNonOverlappingDatesTest()
        {
            var rooms = new List<Room>();
            var books = new List<Booking>();

            var startdate = DateTime.Now;
            var enddate = DateTime.Now.AddDays(1);

            Room r = new Room() { Id = 1, Name = "Carlo" };
            Room rr = new Room() { Id = 2, Name = "Morten" };

            rooms.Add(r);
            rooms.Add(rr);

            Booking b = new Booking() { Id = 1, Room = new List<Room> { r }, StartDate = startdate.AddDays(5), EndDate = enddate.AddDays(5) };
            r.Bookings = new List<Booking> { b };
            books.Add(b);

            var ava = new CheckRoomAvailability(rooms, books);

            var availeblerooms = ava.Check(startdate, enddate);

            Assert.AreEqual(2, availeblerooms.Count);

        }

        // Check for no bookings
        // check for no rooms
        //

        [TestMethod]
        public void FetchUnavailableDatesNoBookings()
        {
            
        }
    }
}
