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
    public class ReviewManagerTest
    {
        private static Mock<IRepository<Review>> mock;

        private static IList<Review> reviews = new List<Review>();

        [ClassInitialize]
        public static void Classinititializer(TestContext context)
        {

            mock = new Mock<IRepository<Review>>();
            mock.Setup(x => x.Create(It.IsAny<Review>())).Callback<Review>((s) => reviews.Add(s));
            mock.Setup(x => x.ReadAll()).Returns(() => reviews.ToList());
            mock.Setup(x => x.Read(It.IsAny<int>())).Returns((int id) => reviews.FirstOrDefault(x => x.Id == id));
            mock.Setup(x => x.Delete(It.IsAny<Review>())).Callback<Review>((s) => reviews.Remove(s));
            mock.Setup(x => x.Update(It.IsAny<Review>()))
                .Callback<Review>((s) => reviews.Insert(reviews.IndexOf(reviews.FirstOrDefault(x => x.Id == s.Id)), s));
        }

        /// <summary>
        /// Executed before each test method is executed.
        /// Ensures each test is executed on an empty repository.
        /// </summary>

        [TestInitialize]
        public void testInitializer()
        {

            reviews.Clear();
        }

        /// <summary>
        /// Test method testing the creation of a ReviewManager with an existing repository.
        /// </summary>

        [TestMethod]
        public void CreateReviewManagerExitingRepositoryTest()
        {
            IRepository<Review> repo = mock.Object;
            ReviewManager rm = new ReviewManager(repo);

            Assert.IsNotNull(rm);
            Assert.AreEqual(0, reviews.Count);
        }

        /// <summary>
        /// Test method testing creation of a ReviewManager with no repository (null).
        /// Expects ArgumentNullException to be thrown.
        /// </summary>

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateReviewManagerNoRepositoryExceptionExcpected()
        {
            IRepository<Review> repo = null;
            ReviewManager rm = new ReviewManager(repo);

            Assert.Fail("Created ReviewManager with null repository");
        }

        /// <summary>
        /// Test method testing adding a new reviews to the repository.
        /// </summary>

        [TestMethod]
        public void AddNewReviewTest()
        {
            IRepository<Review> repo = mock.Object;
            ReviewManager rm = new ReviewManager(repo);

            Review f = new Review() { Id = 1,Name = "anders", Description = "blabla"};
            rm.Create(f);

            Assert.AreEqual(f, rm.Read(1));
            Assert.AreEqual(1, reviews.Count);

        }

        /// <summary>
        /// Test method adding an existing reviews to the repository.
        /// Expects an ArgumentException to be thrown.
        /// </summary>

        [TestMethod]
        public void AddReviewExistingReviewTest()
        {
            IRepository<Review> repo = mock.Object;
            ReviewManager rm = new ReviewManager(repo);

            Review f = new Review() { Id = 1, Name = "anders", Description = "blabla" };
            rm.Create(f);

            try
            {
                rm.Create(f); // try to add the same room again
                Assert.Fail("Added existing reviews to repository");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, reviews.Count);
                Assert.AreEqual(f, rm.Read(1));
            }
        }

        /// <summary>
        /// Test method testing removal of an existing reviews remove.
        /// </summary>

        [TestMethod]
        public void RemoveReviewExistingReviewTest()
        {
            IRepository<Review> repo = mock.Object;
            ReviewManager rm = new ReviewManager(repo);

            Review f = new Review() { Id = 1, Name = "anders", Description = "blabla" };
            Review ff = new Review() { Id = 2, Name = "anders", Description = "muchen" };


            rm.Create(f);
            rm.Create(ff);

            rm.Delete(f);

            Assert.AreEqual(1, reviews.Count);
            Assert.AreEqual(ff, rm.ReadAll()[0]);


        }

        /// <summary>
        /// Test method testing removal of a non-existing reviews.
        /// Expects an ArgumentException to be thrown.
        /// </summary>

        [TestMethod]
        public void RemoveReviewNonExistingReviewTest()
        {
            IRepository<Review> repo = mock.Object;
            ReviewManager rm = new ReviewManager(repo);

            Review f = new Review() { Id = 1, Name = "anders", Description = "blabla" };
            Review ff = new Review() { Id = 2, Name = "anders", Description = "muchen" };


            rm.Create(f);
            try
            {
                rm.Delete(ff);
                Assert.Fail("Removed none existing Review ");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, reviews.Count);
                Assert.AreEqual(f, rm.ReadAll()[0]);
            }
        }
        /// <summary>
        /// Test method testing the retrieval of all reviews from the repository.
        /// </summary>
        [TestMethod]
        public void ReadAllReview_Test()
        {
            IRepository<Review> repo = mock.Object;
            ReviewManager rm = new ReviewManager(repo);

            Review f = new Review() { Id = 1, Name = "anders", Description = "blabla" };
            Review ff = new Review() { Id = 2, Name = "anders", Description = "muchen" };


            rm.Create(f);
            rm.Create(ff);

            IList<Review> result = rm.ReadAll();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(f, result[0]);
            Assert.AreEqual(ff, result[1]);

        }
        /// <summary>
        /// Test method testing retrieval of an existing reviews with a specific Id.
        /// </summary>

        [TestMethod]
        public void GetReviewById_Existing_Review_Test()
        {
            IRepository<Review> repo = mock.Object;
            ReviewManager rm = new ReviewManager(repo);

            Review f = new Review() { Id = 1, Name = "anders", Description = "blabla" };
            Review ff = new Review() { Id = 2, Name = "anders", Description = "muchen" };


            rm.Create(f);
            rm.Create(ff);

            Review result = rm.Read(1);

            Assert.AreEqual(f, result);
        }
        [TestMethod]
        public void UpdateexistingReview()
        {

            IRepository<Review> repo = mock.Object;
            ReviewManager rm = new ReviewManager(repo);

            Review f1 = new Review() { Id = 1, Name = "anders", Description = "blabla" };
            Review f2 = new Review() { Id = 1, Name = "anders", Description = "muchen" };


            rm.Create(f1);
            bool isUpdated = rm.Update(f2);

            var fupdated = rm.Read(1);


            Assert.AreEqual(true, isUpdated);
            Assert.AreEqual(fupdated.Name, f2.Name);
            Assert.AreEqual(fupdated.Description, f2.Description);
            Assert.AreEqual(fupdated.Id, f2.Id);
        }

        [TestMethod]
        public void ReadNonExistingReview()
        {

            IRepository<Review> repo = mock.Object;
            ReviewManager cm = new ReviewManager(repo);

            Review f = new Review() { Id = 1 };

            cm.Create(f);

            try
            {
                cm.Read(0);
                Assert.Fail("Read review with id 0 ");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, reviews.Count);

            }

        }


        [TestMethod]
        public void UpdateNonExistingReview()
        {
            IRepository<Review> repo = mock.Object;
            ReviewManager bm = new ReviewManager(repo);

            Review f  = new Review() {Id = 1};
            Review ff = new Review() {Id = 2 };

            bm.Create(f);
            try
            {
                bool isUpdated = bm.Update(ff);
                Assert.Fail("Non existing Review updated");
            }
            catch (Exception)
            {
                var book = bm.Read(1);
                Assert.AreEqual(f.Id, book.Id);
            }
        }
    }
}
