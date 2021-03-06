﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DLL.DAL;
using DLL.DAL.Entities;
using DLL.DAL.Managers;
using DLL.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestS
{
    [TestClass]
    public class RoomManagerTest
    {
        private static Mock<IRepository<Room>> mock;

        // Internal list of rooms replacing the repository (fake repository)
        private static IList<Room> rooms = new List<Room>();

        [ClassInitialize]

        public static void classinititializer(TestContext context)
        {

            mock = new Mock<IRepository<Room>>();
            mock.Setup(x => x.Create(It.IsAny<Room>())).Callback<Room>((s) => rooms.Add(s));
            mock.Setup(x => x.ReadAll()).Returns(() => rooms.ToList());
            mock.Setup(x => x.Read(It.IsAny<int>())).Returns((int id) => rooms.FirstOrDefault(x => x.Id == id));
            mock.Setup(x => x.Delete(It.IsAny<Room>())).Callback<Room>((s) => rooms.Remove(s));
            mock.Setup(x => x.Update(It.IsAny<Room>()))
                .Callback<Room>((s) => rooms.Insert(rooms.IndexOf(rooms.FirstOrDefault(x => x.Id == s.Id)), s));
        }

        /// <summary>
        /// Executed before each test method is executed.
        /// Ensures each test is executed on an empty repository.
        /// </summary>

        [TestInitialize]
        public void testInitializer()
        {

            rooms.Clear();
        }

        /// <summary>
        /// Test method testing the creation of a RoomManager with an existing repository.
        /// </summary>

        [TestMethod]
        public void CreateRoomManagerExitingRepositoryTest()
        {
            IRepository<Room> repo = mock.Object;
            RoomManager rm = new RoomManager(repo);
            
            Assert.IsNotNull(rm);
            Assert.AreEqual(0, rooms.Count);
        }

        /// <summary>
        /// Test method testing creation of a RoomManager with no repository (null).
        /// Expects ArgumentNullException to be thrown.
        /// </summary>

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateRoomManagerNoRepositoryExceptionExcpected()
        {
            IRepository<Room> repo = null;
            RoomManager rm = new RoomManager(repo);

            Assert.Fail("Created lablaba with null repository");
        }

        /// <summary>
        /// Test method testing adding a new room to the repository.
        /// </summary>

        [TestMethod]
        public void AddNewRoomTest()
        {
            IRepository<Room> repo = mock.Object;
            RoomManager rm = new RoomManager(repo);

            Room r = new Room() { Id = 1, Name = "Room1", Persons = 2, Price = 500, Description = "blabla" };
            rm.Create(r);

            Assert.AreEqual(r, rm.Read(1));
            Assert.AreEqual(1, rooms.Count);

        }

        /// <summary>
        /// Test method adding an existing room to the repository.
        /// Expects an ArgumentException to be thrown.
        /// </summary>

        [TestMethod]
        public void AddRoomExistingRoomTest()
        {
            IRepository<Room> repo = mock.Object;
            RoomManager rm = new RoomManager(repo);

            Room r = new Room() { Id = 1, Name = "Room1", Persons = 2, Price = 500, Description = "blabla" };
            rm.Create(r);

            try
            {
                rm.Create(r); // try to add the same room again
                Assert.Fail("Added existing room to repository");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, rooms.Count);
                Assert.AreEqual(r, rm.Read(1));
            }
        }

        /// <summary>
        /// Test method testing removal of an existing remove.
        /// </summary>

        [TestMethod]
        public void RemoveRoomExistingRoomTest()
        {
            IRepository<Room> repo = mock.Object;
            RoomManager rm = new RoomManager(repo);

            Room r = new Room() { Id = 1, Name = "Room1", Persons = 2, Price = 500, Description = "blabla" };
            Room rr = new Room() { Id = 2, Name = "Room2", Persons = 2, Price = 500, Description = "jigaijgijag" };

            rm.Create(r);
            rm.Create(rr);

            rm.Delete(r);

            Assert.AreEqual(1, rooms.Count);
            Assert.AreEqual(rr, rm.ReadAll()[0]);


        }

        /// <summary>
        /// Test method testing removal of a non-existing room.
        /// Expects an ArgumentException to be thrown.
        /// </summary>

        [TestMethod]
        public void RemoveRoomNonExistingRoomTest()
        {
            IRepository<Room> repo = mock.Object;
            RoomManager rm = new RoomManager(repo);

            Room r = new Room() { Id = 1, Name = "Room1", Persons = 2, Price = 500, Description = "blabla" };
            Room rr = new Room() { Id = 2, Name = "Room2", Persons = 2, Price = 500, Description = "jigaijgijag" };

            rm.Create(r);

            try
            {
                rm.Delete(rr);
                Assert.Fail("Removed none existing Room ");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, rooms.Count);
                Assert.AreEqual(r, rm.ReadAll()[0]);
            }
        }
        /// <summary>
        /// Test method testing the retrieval of all rooms from the repository.
        /// </summary>
        [TestMethod]
        public void ReadAllRooms_Test()
        {
            IRepository<Room> repo = mock.Object;
            RoomManager rm = new RoomManager(repo);

            Room r = new Room() { Id = 1, Name = "Room1", Persons = 2, Price = 500, Description = "blabla" };
            Room rr = new Room() { Id = 2, Name = "Room2", Persons = 2, Price = 500, Description = "jigaijgijag" };

            rm.Create(r);
            rm.Create(rr);

            IList<Room> result = rm.ReadAll();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(r, result[0]);
            Assert.AreEqual(rr, result[1]);

        }
        /// <summary>
        /// Test method testing retrieval of an existing room with a specific Id.
        /// </summary>

        [TestMethod]
        public void GetRoomById_Existing_Room_Test()
        {
            IRepository<Room> repo = mock.Object;
            RoomManager rm = new RoomManager(repo);

            Room r = new Room() { Id = 1, Name = "Room1", Persons = 2, Price = 500, Description = "blabla" };
            Room rr = new Room() { Id = 2, Name = "Room2", Persons = 2, Price = 500, Description = "jigaijgijag" };

            rm.Create(r);
            rm.Create(rr);

            Room result = rm.Read(1);

            Assert.AreEqual(r, result);
        }
        [TestMethod]
        public void UpdateexistingRoom()
        {
            IRepository<Room> repo = mock.Object;
            RoomManager rm = new RoomManager(repo);

            //Create and adds the room
            Room r1 = new Room() { Id = 1, Name = "Toilet", Persons = 2, Price = 500, Description = "Very gross" };
            rm.Create(r1);

            Room r2 = new Room() { Id = 1, Name = "Living room", Persons = 4, Price = 300, Description = "Smooth" };

            bool isUpdated = rm.Update(r2);

            Assert.AreEqual(true, isUpdated);
            Assert.AreEqual(r1.Name, r2.Name);
            Assert.AreEqual(r1.Persons, r2.Persons);
            Assert.AreEqual(r1.Price, r2.Price);
            Assert.AreEqual(r1.Description, r2.Description);
            Assert.AreEqual(r1.Id, r2.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateRoomThatDoesntexist()
        {
            IRepository<Room> repo = mock.Object;
            RoomManager rm = new RoomManager(repo);

            //Create room
            Room r1 = new Room() { Id = 1, Name = "Toilet", Persons = 2, Price = 500, Description = "Very gross" };
            //try update the room which doesnt exist
            rm.Update(r1);

            Assert.Fail("Updated room that didnt exist");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateRoomWithNullParameter()
        {
            IRepository<Room> repo = mock.Object;
            RoomManager rm = new RoomManager(repo);

            rm.Update(null);

            Assert.Fail("Updated room with null");
        }



    }
}
