using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DLL.DAL;
using DLL.DAL.Entities;
using DLL.DAL.Managers;
using DLL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestS
{
    [TestClass]
    public class RoomRepositoryTest
    {
        private static Mock<IRepository<Room>> mock;
        private static IList<Room> rooms = new List<Room>();

        [ClassInitialize]

        public static void classinititializer(TestContext context)
        {

            mock = new Mock<IRepository<Room>>();
            mock.Setup(x => x.Create(It.IsAny<Room>())).Callback<Room>((s) => rooms.Add(s));
            mock.Setup(x => x.ReadAll()).Returns(() => rooms.ToList());
            mock.Setup(x => x.Read(It.IsAny<Room>())).Returns((Room r) => rooms.FirstOrDefault(x => x.Id == r.Id));
            mock.Setup(x => x.Delete(It.IsAny<Room>())).Callback<Room>((s) => rooms.Remove(s));
            mock.Setup(x => x.Update(It.IsAny<Room>()))
                .Callback<Room>((s) => rooms.Insert(rooms.IndexOf(rooms.FirstOrDefault(x => x.Id == s.Id)), s));
        }

        [TestInitialize]
        public void testInitializer()
        {

            rooms.Clear();
        }

        [TestMethod]
        public void CreateRoomManagerExitingRepositoryTest()
        {
            IRepository<Room> repo = mock.Object;
            RoomManager rm = new RoomManager(repo);

            Assert.IsNotNull(rm);
            Assert.AreEqual(0, rooms.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateStudentManagerNoRepositoryExceptionExcpected()
        {
            IRepository<Room> repo = null;
            RoomManager rm = new RoomManager(repo);

            Assert.Fail("Created lablaba with null repository");
        }


        [TestMethod]
        public void AddNewRoomTest()
        {
            IRepository<Room> repo = mock.Object;
            RoomManager rm = new RoomManager(repo);


            Room r = new Room()
            {
                Id = 1,
                Name = "Room1",
                Persons = 2,
                Price = 500,
                Description = "blabla"
            };



            rm.Create(r);

            Assert.AreEqual(r, rm.Read(r));
            Assert.AreEqual(1, rooms.Count);

        }

        [TestMethod]
        public void AddRoomExistingRoomTest()
        {
            IRepository<Room> repo = mock.Object;
            RoomManager rm = new RoomManager(repo);

            Room r = new Room()
            {
                Id = 1,
                Name = "Room1",
                Persons = 2,
                Price = 500,
                Description = "blabla"
            };



            rm.Create(r);

            try
            {
                rm.Create(r);
                Assert.Fail("Added existing room to repository");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, rooms.Count);
                Assert.AreEqual(r, rm.Read(r));
            }
        }
    }
}
