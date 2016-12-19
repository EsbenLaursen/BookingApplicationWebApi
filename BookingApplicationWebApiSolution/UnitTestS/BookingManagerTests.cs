using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using DLL;
using DLL.DAL.Entities;
using DLL.DAL.Managers;
using DLL.DAL.Repositories;
using Moq;

namespace UnitTestS
{
    [TestClass]
    public class BookingManagerTests
    {
        private static IList<Booking> bookings = new List<Booking>();

        private static Mock<IRepository<Booking>> mock;

        [ClassInitialize]
        public static void ClassInitializer(TestContext context)
        {
            mock = new Mock<IRepository<Booking>>();
            mock.Setup(x => x.Create(It.IsAny<Booking>())).Callback<Booking>((c) => bookings.Add(c));
            mock.Setup(x => x.ReadAll()).Returns(() => bookings.ToList());
            mock.Setup(x => x.Read(It.IsAny<int>())).Returns((int id) => bookings.FirstOrDefault(x => x.Id == id));
            mock.Setup(x => x.Delete(It.IsAny<Booking>())).Callback<Booking>((s) => bookings.Remove(s));
            mock.Setup(x => x.Update(It.IsAny<Booking>()))
                .Callback<Booking>(
                    (s) => bookings.Insert(bookings.IndexOf(bookings.FirstOrDefault(x => x.Id == s.Id)), s));
        }


        [TestInitialize]
        public void TestInitializer()
        {
            bookings.Clear();
        }

        [TestMethod]
        public void CreateBookingManagerExitingRepositoryTest()
        {
            IRepository<Booking> repo = mock.Object;
            BookingManager bm = new BookingManager(repo);

            Assert.IsNotNull(bm);
            Assert.AreEqual(0, bookings.Count);
        }

        /// <summary>
        /// Test method testing creation of a BookingManager with no repository (null).
        /// Expects ArgumentNullException to be thrown.
        /// </summary>

        [TestMethod]
        public void ReadNonExistingBooking()
        {
            IRepository<Booking> repo = mock.Object;
            BookingManager bm = new BookingManager(repo);
            var rooms = new List<Room>();
            rooms.Add(new Room());
            Booking b = new Booking()
            {
                Id = 1,
                Breakfast = true,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Room = rooms
            };
            bm.Create(b);

            try
            {
                bm.Read(0);
                Assert.Fail("Read booking with id 0 ");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, bookings.Count);

            }

        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateBookingManagerNoRepositoryExceptionExcpected()
        {
            IRepository<Booking> repo = null;
            BookingManager bm = new BookingManager(repo);

            Assert.Fail("Created BookingManager with null repository");
        }

        /// <summary>
        /// Test method testing adding a new booking to the repository.
        /// </summary>

        [TestMethod]
        public void AddNewBokingTest()
        {
            IRepository<Booking> repo = mock.Object;
            BookingManager bm = new BookingManager(repo);
            var rooms = new List<Room>();
            rooms.Add(new Room());
            Booking b = new Booking()
            {
                Id = 1,
                Breakfast = true,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Room = rooms
            };
            bm.Create(b);

            Assert.AreEqual(b, bm.Read(1));
            Assert.AreEqual(1, bookings.Count);

        }

        /// <summary>
        /// Test method adding an existing booking to the repository.
        /// Expects an ArgumentException to be thrown.
        /// </summary>

        [TestMethod]
        public void AddBookingExistingBookingTest()
        {
            IRepository<Booking> repo = mock.Object;
            BookingManager bm = new BookingManager(repo);

            var rooms = new List<Room>();
            rooms.Add(new Room());

            Booking b = new Booking()
            {
                Id = 1,
                Breakfast = true,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Room = rooms
            };
            bm.Create(b);

            try
            {
                bm.Create(b); // try to add the same booking again
                Assert.Fail("Added existing booking to repository");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, bookings.Count);
                Assert.AreEqual(b, bm.Read(1));
            }
        }

        [TestMethod]
        public void AddBookingNoRoomsTest()
        {
            IRepository<Booking> repo = mock.Object;
            BookingManager bm = new BookingManager(repo);



            Booking b = new Booking() {Id = 1, Breakfast = true, StartDate = DateTime.Today, EndDate = DateTime.Now};

            try
            {
                bm.Create(b); // try to add the same booking again
                Assert.Fail("Added booking with no rooms to repository");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(0, bookings.Count);
                Assert.AreEqual(null, bm.Read(1));
            }
        }

        /// <summary>
        /// Test method testing removal of an existing booking remove.
        /// </summary>

        [TestMethod]
        public void RemoveBookingExistingBokingTest()
        {

            IRepository<Booking> repo = mock.Object;
            BookingManager bm = new BookingManager(repo);

            var rooms = new List<Room>();
            rooms.Add(new Room());
            Booking b = new Booking()
            {
                Id = 1,
                Breakfast = true,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Room = rooms
            };
            Booking bb = new Booking()
            {
                Id = 2,
                Breakfast = false,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Room = rooms
            };

            bm.Create(b);
            bm.Create(bb);

            bm.Delete(b);

            Assert.AreEqual(1, bookings.Count);
            Assert.AreEqual(bb, bm.ReadAll()[0]);


        }

        /// <summary>
        /// Test method testing removal of a non-existing booking.
        /// Expects an ArgumentException to be thrown.
        /// </summary>

        [TestMethod]
        public void RemoveBookingNonExistingBookingTest()
        {
            IRepository<Booking> repo = mock.Object;
            BookingManager bm = new BookingManager(repo);

            var rooms = new List<Room>();
            rooms.Add(new Room());
            Booking b = new Booking()
            {
                Id = 1,
                Breakfast = true,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Room = rooms
            };
            Booking bb = new Booking()
            {
                Id = 2,
                Breakfast = false,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Room = rooms
            };

            bm.Create(b);

            try
            {
                bm.Delete(bb);
                Assert.Fail("Removed none existing booking ");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, bookings.Count);
                Assert.AreEqual(b, bm.ReadAll()[0]);
            }
        }

        /// <summary>
        /// Test method testing the retrieval of all bookings from the repository.
        /// </summary>
        [TestMethod]
        public void ReadAllBooking_Test()
        {
            var rooms = new List<Room>();
            rooms.Add(new Room());
            IRepository<Booking> repo = mock.Object;
            BookingManager bm = new BookingManager(repo);

            Booking b = new Booking()
            {
                Id = 1,
                Breakfast = true,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Room = rooms
            };
            Booking bb = new Booking()
            {
                Id = 2,
                Breakfast = false,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Room = rooms
            };

            bm.Create(b);
            bm.Create(bb);

            IList<Booking> result = bm.ReadAll();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(b, result[0]);
            Assert.AreEqual(bb, result[1]);

        }

        /// <summary>
        /// Test method testing retrieval of an existing Booking with a specific Id.
        /// </summary>

        [TestMethod]
        public void GetBookingById_Existing_Booking_Test()
        {

            IRepository<Booking> repo = mock.Object;
            BookingManager bm = new BookingManager(repo);

            var rooms = new List<Room>();
            rooms.Add(new Room());
            Booking b = new Booking()
            {
                Id = 1,
                Breakfast = true,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Room = rooms
            };
            Booking bb = new Booking()
            {
                Id = 2,
                Breakfast = false,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Room = rooms
            };

            bm.Create(b);
            bm.Create(bb);

            Booking result = bm.Read(1);

            Assert.AreEqual(b, result);
        }

        [TestMethod]
        public void UpdateexistingBookingTest()
        {
            IRepository<Booking> repo = mock.Object;
            BookingManager bm = new BookingManager(repo);

            var rooms = new List<Room>();
            rooms.Add(new Room());
            //Create and adds the booking
            Booking b = new Booking()
            {
                Id = 1,
                Breakfast = true,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Room = rooms
            };
            bm.Create(b);
            Booking bb = new Booking()
            {
                Id = 1,
                Breakfast = false,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Room = rooms
            };

            bool isUpdated = bm.Update(bb);

            Assert.AreEqual(true, isUpdated);
            Assert.AreEqual(b.StartDate, bb.StartDate);
            Assert.AreEqual(b.EndDate, bb.EndDate);
            Assert.AreEqual(b.Breakfast, bb.Breakfast);
            Assert.AreEqual(b.Id, bb.Id);
        }

        [TestMethod]
        public void UpdateNonExistingBookingTest()
        {
            IRepository<Booking> repo = mock.Object;
            BookingManager bm = new BookingManager(repo);

            var rooms = new List<Room>();
            rooms.Add(new Room());
            //Create and adds the booking
            Booking b = new Booking()
            {
                Id = 1,
                Breakfast = true,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Room = rooms
            };
            bm.Create(b);
            Booking bb = new Booking()
            {
                Id = 2,
                Breakfast = false,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Room = rooms
            };

           

            try
            {
                bool isUpdated = bm.Update(bb);
                Assert.Fail("Non existing booking updated");
            }
            catch (Exception)
            {
                var book = bm.Read(1);
                Assert.AreEqual(b.Id, book.Id);
                Assert.AreEqual(b.Room, book.Room);
                Assert.AreEqual(b.Breakfast, book.Breakfast);
                Assert.AreEqual(b.Customer, book.Customer);
                Assert.AreEqual(b.EndDate, book.EndDate);
                Assert.AreEqual(b.StartDate, book.StartDate);
            }
        }
    }
}
    
