using System;
using System.Collections.Generic;
using System.Linq;
using DLL.DAL.Entities;
using DLL.DAL.Managers;
using DLL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestS
{
    [TestClass]
    public class FootCareRepositoryTest
    {
        private static Mock<IRepository<FootCare>> mock;

        private static IList<FootCare> footcare = new List<FootCare>();

        [ClassInitialize]
        public static void Classinititializer(TestContext context)
        {

            mock = new Mock<IRepository<FootCare>>();
            mock.Setup(x => x.Create(It.IsAny<FootCare>())).Callback<FootCare>((s) => footcare.Add(s));
            mock.Setup(x => x.ReadAll()).Returns(() => footcare.ToList());
            mock.Setup(x => x.Read(It.IsAny<int>())).Returns((int id) => footcare.FirstOrDefault(x => x.Id == id));
            mock.Setup(x => x.Delete(It.IsAny<FootCare>())).Callback<FootCare>((s) => footcare.Remove(s));
            mock.Setup(x => x.Update(It.IsAny<FootCare>()))
                .Callback<FootCare>((s) => footcare.Insert(footcare.IndexOf(footcare.FirstOrDefault(x => x.Id == s.Id)), s));
        }

        /// <summary>
        /// Executed before each test method is executed.
        /// Ensures each test is executed on an empty repository.
        /// </summary>

        [TestInitialize]
        public void testInitializer()
        {

            footcare.Clear();
        }

        /// <summary>
        /// Test method testing the creation of a FootCareManager with an existing repository.
        /// </summary>

        [TestMethod]
        public void CreateFootCareManagerExitingRepositoryTest()
        {
            IRepository<FootCare> repo = mock.Object;
            FootCareManager fcm = new FootCareManager(repo);

            Assert.IsNotNull(fcm);
            Assert.AreEqual(0, footcare.Count);
        }

        /// <summary>
        /// Test method testing creation of a FootCareManager with no repository (null).
        /// Expects ArgumentNullException to be thrown.
        /// </summary>

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateFootCareManagerNoRepositoryExceptionExcpected()
        {
            IRepository<FootCare> repo = null;
            FootCareManager fcm = new FootCareManager(repo);

            Assert.Fail("Created footcareManager with null repository");
        }

        /// <summary>
        /// Test method testing adding a new footcare to the repository.
        /// </summary>

        [TestMethod]
        public void AddNewFootCareTest()
        {
            IRepository<FootCare> repo = mock.Object;
            FootCareManager fcm = new FootCareManager(repo);

            FootCare f = new FootCare() { Id = 1, Name = "footcare1", Duration = 2, Price = 500, Description = "blabla" };
            fcm.Create(f);

            Assert.AreEqual(f, fcm.Read(1));
            Assert.AreEqual(1, footcare.Count);

        }

        /// <summary>
        /// Test method adding an existing footcare to the repository.
        /// Expects an ArgumentException to be thrown.
        /// </summary>

        [TestMethod]
        public void AddFootCareExistingFootCareTest()
        {
            IRepository<FootCare> repo = mock.Object;
            FootCareManager fcm = new FootCareManager(repo);

            FootCare f = new FootCare() { Id = 1, Name = "footcare1", Duration = 2, Price = 500, Description = "blabla" };
            fcm.Create(f);

            try
            {
                fcm.Create(f); // try to add the same room again
                Assert.Fail("Added existing footcare to repository");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, footcare.Count);
                Assert.AreEqual(f, fcm.Read(1));
            }
        }

        /// <summary>
        /// Test method testing removal of an existing footcare remove.
        /// </summary>

        [TestMethod]
        public void RemoveFootCareExistingFootCareTest()
        {
            IRepository<FootCare> repo = mock.Object;
            FootCareManager fcm = new FootCareManager(repo);

            FootCare f = new FootCare() { Id = 1, Name = "footcare1", Duration = 2, Price = 500, Description = "blabla" };
            FootCare ff = new FootCare() { Id = 2, Name = "footcare2", Duration = 2, Price = 500, Description = "jigaijgijag" };

            fcm.Create(f);
            fcm.Create(ff);

            fcm.Delete(f);

            Assert.AreEqual(1, footcare.Count);
            Assert.AreEqual(ff, fcm.ReadAll()[0]);


        }

        /// <summary>
        /// Test method testing removal of a non-existing footcare.
        /// Expects an ArgumentException to be thrown.
        /// </summary>

        [TestMethod]
        public void RemoveFootCareNonExistingFootCareTest()
        {
            IRepository<FootCare> repo = mock.Object;
            FootCareManager fcm = new FootCareManager(repo);

            FootCare f = new FootCare() { Id = 1, Name = "footcare1", Duration = 2, Price = 500, Description = "blabla" };
            FootCare ff = new FootCare() { Id = 2, Name = "footcare2", Duration = 2, Price = 500, Description = "jigaijgijag" };

            fcm.Create(f);

            try
            {
                fcm.Delete(ff);
                Assert.Fail("Removed none existing Footcare ");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, footcare.Count);
                Assert.AreEqual(f, fcm.ReadAll()[0]);
            }
        }
        /// <summary>
        /// Test method testing the retrieval of all footcare from the repository.
        /// </summary>
        [TestMethod]
        public void ReadAllFootCare_Test()
        {
            IRepository<FootCare> repo = mock.Object;
            FootCareManager fcm = new FootCareManager(repo);

            FootCare f = new FootCare() { Id = 1, Name = "Room1", Duration = 2, Price = 500, Description = "blabla" };
            FootCare ff = new FootCare() { Id = 2, Name = "Room2", Duration = 2, Price = 500, Description = "jigaijgijag" };

            fcm.Create(f);
            fcm.Create(ff);

            IList<FootCare> result = fcm.ReadAll();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(f, result[0]);
            Assert.AreEqual(ff, result[1]);

        }
        /// <summary>
        /// Test method testing retrieval of an existing footcare with a specific Id.
        /// </summary>

        [TestMethod]
        public void GetFootCareById_Existing_FootCare_Test()
        {
            IRepository<FootCare> repo = mock.Object;
            FootCareManager fcm = new FootCareManager(repo);

            FootCare f = new FootCare() { Id = 1, Name = "Room1", Duration = 2, Price = 500, Description = "blabla" };
            FootCare ff = new FootCare() { Id = 2, Name = "Room2", Duration = 2, Price = 500, Description = "jigaijgijag" };

            fcm.Create(f);
            fcm.Create(ff);

            FootCare result = fcm.Read(1);

            Assert.AreEqual(f, result);
        }
        [TestMethod]
        public void UpdateexistingFootCare()
        {

            IRepository<FootCare> repo = mock.Object;
            FootCareManager fcm = new FootCareManager(repo);


            //Create and adds the footcare
            FootCare f1 = new FootCare() { Id = 1, Name = "left foot", Duration = 2, Price = 500, Description = "Very gross" };
            fcm.Create(f1);

            FootCare f2 = new FootCare() { Id = 1, Name = "right foot", Duration = 4, Price = 300, Description = "Smooth" };

            bool isUpdated = fcm.Update(f2);



            Assert.AreEqual(true, isUpdated);
            Assert.AreEqual(f1.Name, f2.Name);
            Assert.AreEqual(f1.Duration, f2.Duration);
            Assert.AreEqual(f1.Price, f2.Price);
            Assert.AreEqual(f1.Description, f2.Description);
            Assert.AreEqual(f1.Id, f2.Id);
        }
    }
}
