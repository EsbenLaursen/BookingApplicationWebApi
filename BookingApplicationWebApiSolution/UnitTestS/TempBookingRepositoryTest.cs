using System;
using System.Collections.Generic;
using System.Linq;
using DLL.DAL.Entities;
using DLL.DAL.Managers;
using DLL.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestS
{
    [TestClass]
    public class TempBookingRepositoryTest
    {
        [TestClass]
        public class TempBookingRepositoryTests
        {
            private static IList<TemporaryBooking> tempbooking = new List<TemporaryBooking>();

            private static Mock<IRepository<TemporaryBooking>> mock;

            [ClassInitialize]
            public static void ClassInitializer(TestContext context)
            {
                mock = new Mock<IRepository<TemporaryBooking>>();
                mock.Setup(x => x.Create(It.IsAny<TemporaryBooking>())).Callback<TemporaryBooking>((c) => tempbooking.Add(c));
                mock.Setup(x => x.ReadAll()).Returns(() => tempbooking.ToList());
                mock.Setup(x => x.Read(It.IsAny<int>())).Returns((int id) => tempbooking.FirstOrDefault(x => x.Id == id));
                mock.Setup(x => x.Delete(It.IsAny<TemporaryBooking>())).Callback<TemporaryBooking>((s) => tempbooking.Remove(s));
                mock.Setup(x => x.Update(It.IsAny<TemporaryBooking>()))
                    .Callback<TemporaryBooking>((s) => tempbooking.Insert(tempbooking.IndexOf(tempbooking.FirstOrDefault(x => x.Id == s.Id)), s));
            }


            [TestInitialize]
            public void TestInitializer()
            {
                tempbooking.Clear();
            }

            [TestMethod]
            public void CreateTempBookingManagerExitingRepositoryTest()
            {
                IRepository<TemporaryBooking> repo = mock.Object;
                TempBookingManager bm = new TempBookingManager(repo);

                Assert.IsNotNull(bm);
                Assert.AreEqual(0, tempbooking.Count);
            }

            /// <summary>
            /// Test method testing creation of a BookingManager with no repository (null).
            /// Expects ArgumentNullException to be thrown.
            /// </summary>

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void CreateTempBookingManagerNoRepositoryExceptionExcpected()
            {
                IRepository<TemporaryBooking> repo = null;
                TempBookingManager tbm = new TempBookingManager(repo);

                Assert.Fail("Created BookingManager with null repository");
            }

            /// <summary>
            /// Test method testing adding a new booking to the repository.
            /// </summary>

            [TestMethod]
            public void AddNewTempBokingTest()
            {
                IRepository<TemporaryBooking> repo = mock.Object;
                TempBookingManager tbm = new TempBookingManager(repo);
                var rooms = new List<Room>();
                rooms.Add(new Room());
                TemporaryBooking tb = new TemporaryBooking() { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Now, Rooms = rooms};
                tbm.Create(tb);

                Assert.AreEqual(tb, tbm.Read(1));
                Assert.AreEqual(1, tempbooking.Count);

            }

            /// <summary>
            /// Test method adding an existing booking to the repository.
            /// Expects an ArgumentException to be thrown.
            /// </summary>

            [TestMethod]
            public void AddTempBookingExistingTempBookingTest()
            {
                IRepository<TemporaryBooking> repo = mock.Object;
                TempBookingManager tbm = new TempBookingManager(repo);

                var rooms = new List<Room>();
                rooms.Add(new Room());
                TemporaryBooking tb = new TemporaryBooking() { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Now, Rooms = rooms};
                tbm.Create(tb);

                try
                {
                    tbm.Create(tb); // try to add the same booking again
                    Assert.Fail("Added existing booking to repository");
                }
                catch (ArgumentException)
                {
                    Assert.AreEqual(1, tempbooking.Count);
                    Assert.AreEqual(tb, tbm.Read(1));
                }
            }
            [TestMethod]
            public void AddBookingNoRoomsTest()
            {
                IRepository<TemporaryBooking> repo = mock.Object;
                var bm = new TempBookingManager(repo);



                var b = new TemporaryBooking() { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Now};

                try
                {
                    bm.Create(b); // try to add the same booking again
                    Assert.Fail("Added booking with no rooms to repository");
                }
                catch (ArgumentException)
                {
                    Assert.AreEqual(0, tempbooking.Count);
                    Assert.AreEqual(null, bm.Read(1));
                }
            }

            /// <summary>
            /// Test method testing removal of an existing booking remove.
            /// </summary>

            [TestMethod]
            public void RemoveBookingExistingTempBokingTest()
            {
                IRepository<TemporaryBooking> repo = mock.Object;
                TempBookingManager tbm = new TempBookingManager(repo);

                var rooms = new List<Room>();
                rooms.Add(new Room());

                TemporaryBooking tb = new TemporaryBooking() { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Now, Rooms = rooms};
                TemporaryBooking tbb = new TemporaryBooking() { Id = 2, StartDate = DateTime.Today, EndDate = DateTime.Now, Rooms = rooms};

                tbm.Create(tb);
                tbm.Create(tbb);

                tbm.Delete(tb);

                Assert.AreEqual(1, tempbooking.Count);
                Assert.AreEqual(tbb, tbm.ReadAll()[0]);


            }

            /// <summary>
            /// Test method testing removal of a non-existing booking.
            /// Expects an ArgumentException to be thrown.
            /// </summary>

            [TestMethod]
            public void RemoveBookingNonExistingTempBookingTest()
            {
                IRepository<TemporaryBooking> repo = mock.Object;
                TempBookingManager tbm = new TempBookingManager(repo);

                var rooms = new List<Room>();
                rooms.Add(new Room());

                TemporaryBooking tb = new TemporaryBooking() { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Now, Rooms = rooms};
                TemporaryBooking tbb = new TemporaryBooking() { Id = 2, StartDate = DateTime.Today, EndDate = DateTime.Now, Rooms = rooms};

                tbm.Create(tb);

                try
                {
                    tbm.Delete(tbb);
                    Assert.Fail("Removed none existing booking ");
                }
                catch (ArgumentException)
                {
                    Assert.AreEqual(1, tempbooking.Count);
                    Assert.AreEqual(tb, tbm.ReadAll()[0]);
                }
            }
            /// <summary>
            /// Test method testing the retrieval of all bookings from the repository.
            /// </summary>
            [TestMethod]
            public void ReadAllTempBooking_Test()
            {
                IRepository<TemporaryBooking> repo = mock.Object;
                TempBookingManager tbm = new TempBookingManager(repo);

                var rooms = new List<Room>();
                rooms.Add(new Room());

                TemporaryBooking tb = new TemporaryBooking() { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Now, Rooms = rooms};
                TemporaryBooking tbb = new TemporaryBooking() { Id = 2, StartDate = DateTime.Today, EndDate = DateTime.Now, Rooms = rooms};

                tbm.Create(tb);
                tbm.Create(tbb);

                IList<TemporaryBooking> result = tbm.ReadAll();

                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(tb, result[0]);
                Assert.AreEqual(tbb, result[1]);

            }
            /// <summary>
            /// Test method testing retrieval of an existing Booking with a specific Id.
            /// </summary>

            [TestMethod]
            public void GetTempBookingById_Existing_TempBooking_Test()
            {
                IRepository<TemporaryBooking> repo = mock.Object;
                TempBookingManager tbm = new TempBookingManager(repo);

                var rooms = new List<Room>();
                rooms.Add(new Room());

                TemporaryBooking tb = new TemporaryBooking() { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Now, Rooms = rooms};
                TemporaryBooking tbb = new TemporaryBooking() { Id = 2, StartDate = DateTime.Today, EndDate = DateTime.Now, Rooms = rooms};

                tbm.Create(tb);
                tbm.Create(tbb);

                TemporaryBooking result = tbm.Read(1);

                Assert.AreEqual(tb, result);
            }
            [TestMethod]
            public void UpdateexistingTempBooking()
            {
                IRepository<TemporaryBooking> repo = mock.Object;
                TempBookingManager tbm = new TempBookingManager(repo);

                var rooms = new List<Room>();
                rooms.Add(new Room());

                TemporaryBooking tb = new TemporaryBooking() { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Now, Rooms = rooms};
                tbm.Create(tb);
                TemporaryBooking tbb = new TemporaryBooking() { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Now.AddDays(1), Rooms = rooms};

                bool isUpdated = tbm.Update(tbb);
                TemporaryBooking Updated = tbm.Read(1);

                Assert.AreEqual(true, isUpdated);
                Assert.AreEqual(Updated.StartDate, tbb.StartDate);
                Assert.AreEqual(Updated.EndDate, tbb.EndDate);
                Assert.AreEqual(Updated.Id, tbb.Id);
            }
        }
    }


}
