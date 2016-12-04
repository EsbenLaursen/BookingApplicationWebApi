using System;
using System.Text;
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
    public class ImageRepositoryTest
    {
        private static Mock<IRepository<Image>> mock;

        // Internal list of Images replacing the repository (fake repository)
        private static IList<Image> images = new List<Image>();

        [ClassInitialize]

        public static void classinititializer(TestContext context)
        {

            mock = new Mock<IRepository<Image>>();
            mock.Setup(x => x.Create(It.IsAny<Image>())).Callback<Image>((s) => images.Add(s));
            mock.Setup(x => x.ReadAll()).Returns(() => images.ToList());
            mock.Setup(x => x.Read(It.IsAny<int>())).Returns((int id) => images.FirstOrDefault(x => x.ImageId == id));
            mock.Setup(x => x.Delete(It.IsAny<Image>())).Callback<Image>((s) => images.Remove(s));
            mock.Setup(x => x.Update(It.IsAny<Image>()))
                .Callback<Image>((s) => images.Insert(images.IndexOf(images.FirstOrDefault(x => x.ImageId == s.ImageId)), s));
        }

        /// <summary>
        /// Executed before each test method is executed.
        /// Ensures each test is executed on an empty repository.
        /// </summary>

        [TestInitialize]
        public void testInitializer()
        {

            images.Clear();
        }

        /// <summary>
        /// Test method testing the creation of a ImageManager with an existing repository.
        /// </summary>

        [TestMethod]
        public void CreateImageManagerExitingRepositoryTest()
        {
            IRepository<Image> repo = mock.Object;
            ImageManager im = new ImageManager(repo);

            Assert.IsNotNull(im);
            Assert.AreEqual(0, images.Count);
        }

        /// <summary>
        /// Test method testing creation of a ImageManager with no repository (null).
        /// Expects ArgumentNullException to be thrown.
        /// </summary>

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateImageManagerNoRepositoryExceptionExcpected()
        {
            IRepository<Image> repo = null;
            ImageManager im = new ImageManager(repo);


            Assert.Fail("Created ImageManager with null repository");
        }

        /// <summary>
        /// Test method testing adding a new Image to the repository.
        /// </summary>

        [TestMethod]
        public void AddNewImageTest()
        {
            IRepository< Image > repo = mock.Object;
            ImageManager im = new ImageManager(repo);


            Image i = new Image() { ImageId = 1, FileName = "Image" };
            im.Create(i);

            Assert.AreEqual(i, im.Read(1));
            Assert.AreEqual(1, images.Count);

        }

        /// <summary>
        /// Test method adding an existing image to the repository.
        /// Expects an ArgumentException to be thrown.
        /// </summary>

        [TestMethod]
        public void AddImageExistingImageTest()
        {
            IRepository< Image > repo = mock.Object;
            ImageManager im = new ImageManager(repo);


            Image i = new Image() { ImageId = 1, FileName = "Image" };
            im.Create(i);

            try
            {
                im.Create(i); // try to add the same image again
                Assert.Fail("Added existing image to repository");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, images.Count);
                Assert.AreEqual(i, im.Read(1));
            }
        }

        /// <summary>
        /// Test method testing removal of an existing Image.
        /// </summary>

        [TestMethod]
        public void RemoveImageExistingImageTest()
        {
            IRepository<Image> repo = mock.Object;
            ImageManager im = new ImageManager(repo);


            Image i = new Image() { ImageId = 1, FileName = "Image" };

            Image ii = new Image() { ImageId = 2, FileName = "Image" };

            im.Create(i);
            im.Create(ii);

            im.Delete(i);

            Assert.AreEqual(1, images.Count);
            Assert.AreEqual(ii, im.ReadAll()[0]);


        }

        /// <summary>
        /// Test method testing removal of a non-existing Image.
        /// Expects an ArgumentException to be thrown.
        /// </summary>

        [TestMethod]
        public void RemoveImageNonExistingImageTest()
        {
            IRepository<Image> repo = mock.Object;
            ImageManager im = new ImageManager(repo);


            Image i = new Image() { ImageId = 1, FileName = "Image" };

            Image ii = new Image() { ImageId = 2, FileName = "Image" };

            im.Create(i);

            try
            {
                im.Delete(ii);
                Assert.Fail("Removed none existing Image ");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, images.Count);
                Assert.AreEqual(i, im.ReadAll()[0]);
            }
        }
        /// <summary>
        /// Test method testing the retrieval of all images from the repository.
        /// </summary>
        [TestMethod]
        public void ReadAllImages_Test()
        {
            IRepository<Image> repo = mock.Object;
            ImageManager im = new ImageManager(repo);


            Image i = new Image() { ImageId = 1, FileName = "Image" };

            Image ii = new Image() { ImageId = 2, FileName = "Image" };

            im.Create(i);
            im.Create(ii);

            IList<Image> result = im.ReadAll();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(i, result[0]);
            Assert.AreEqual(ii, result[1]);

        }
        /// <summary>
        /// Test method testing retrieval of an existing image with a specific Id.
        /// </summary>

        [TestMethod]
        public void GetImageById_Existing_Image_Test()
        {
            IRepository<Image> repo = mock.Object;
            ImageManager im = new ImageManager(repo);


            Image i = new Image() { ImageId = 1, FileName = "Image" };

            Image ii = new Image() { ImageId = 2, FileName = "Image" };

            im.Create(i);
            im.Create(ii);

            Image result = im.Read(1);

            Assert.AreEqual(i, result);
        }
        /// <summary>
        /// Testing the updating of an existing Image
        /// </summary>

        [TestMethod]
        public void UpdateexistingImage()
        {
            IRepository<Image> repo = mock.Object;
            ImageManager im = new ImageManager(repo);

            //Create and adds the room
            Image i = new Image() { ImageId = 1, FileName = "Image" };
            im.Create(i);

            Image ii = new Image() { ImageId = 1, FileName = "Image2" };

            bool isUpdated = im.Update(ii);

            Assert.AreEqual(true, isUpdated);
            Assert.AreEqual(i.FileName, ii.FileName);
           Assert.AreEqual(i.ImageId, ii.ImageId);
        }

        /// <summary>
        /// Test method testing the update of an Image that doesnt exist
        /// </summary>

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateImageThatDoesntexist()
        {
            IRepository<Image> repo = mock.Object;
            ImageManager im = new ImageManager(repo);

            //Create room
            Image i = new Image() { ImageId = 1, FileName = "Image" };
            //try update the room which doesnt exist
            im.Update(i);

            Assert.Fail("Updated Image that didnt exist");
        }

        /// <summary>
        /// Testing Image with null parameter
        /// </summary>

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateRoomWithNullParameter()
        {
            IRepository<Image> repo = mock.Object;
            ImageManager im = new ImageManager(repo);

            im.Update(null);

            Assert.Fail("Updated image with null");
        }

    }
}
