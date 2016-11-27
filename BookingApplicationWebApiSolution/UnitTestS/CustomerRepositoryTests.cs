using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DLL.DAL.Entities;
using System.Collections.Generic;
using DLL.Repositories;
using Moq;
using System.Linq;
using DLL.DAL.Managers;

namespace UnitTestS
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        private static IList<Customer> customers = new List<Customer>();

        private static Mock<IRepository<Customer>> mock;

        [ClassInitialize]
        public static void ClassInitializer(TestContext context)
        {
            mock = new Mock<IRepository<Customer>>();
            mock.Setup(x => x.Create(It.IsAny<Customer>())).Callback<Customer>((c) => customers.Add(c));
            mock.Setup(x => x.ReadAll()).Returns(() => customers.ToList());
            mock.Setup(x => x.Read(It.IsAny<int>())).Returns((int id) => customers.FirstOrDefault(x => x.Id == id));
            mock.Setup(x => x.Delete(It.IsAny<Customer>())).Callback<Customer>((s) => customers.Remove(s));
            mock.Setup(x => x.Update(It.IsAny<Customer>()))
                .Callback<Customer>((s) => customers.Insert(customers.IndexOf(customers.FirstOrDefault(x => x.Id == s.Id)), s));
        }


        [TestInitialize]
        public void TestInitializer()
        {
            customers.Clear();
        }

        [TestMethod]
        public void CreateCustomerManagerExitingRepositoryTest()
        {
            IRepository<Customer> repo = mock.Object;
            CustomerManager cm = new CustomerManager(repo);

            Assert.IsNotNull(cm);
            Assert.AreEqual(0, customers.Count);
        }

        /// <summary>
        /// Test method testing creation of a CustomereManager with no repository (null).
        /// Expects ArgumentNullException to be thrown.
        /// </summary>

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateCustomerManagerNoRepositoryExceptionExcpected()
        {
            IRepository<Customer> repo = null;
            CustomerManager cm = new CustomerManager(repo);

            Assert.Fail("Created footcareManager with null repository");
        }

        /// <summary>
        /// Test method testing adding a new customer to the repository.
        /// </summary>

        [TestMethod]
        public void AddNewCustomerTest()
        {
            IRepository<Customer> repo = mock.Object;
            CustomerManager cm = new CustomerManager(repo);

            Customer f = new Customer() { Id = 1, Firstname = "Kenny", Lastname = "kühl", Email="Kuhlefar@gmail.com", PhoneNr="329573402" };
            cm.Create(f);

            Assert.AreEqual(f, cm.Read(1));
            Assert.AreEqual(1, customers.Count);

        }

        /// <summary>
        /// Test method adding an existing customer to the repository.
        /// Expects an ArgumentException to be thrown.
        /// </summary>

        [TestMethod]
        public void AddCustomerExistingFootCareTest()
        {
            IRepository<Customer> repo = mock.Object;
            CustomerManager cm = new CustomerManager(repo);

            Customer f = new Customer() { Id = 1, Firstname = "Kenny", Lastname = "kühl", Email = "Kuhlefar@gmail.com", PhoneNr = "329573402" };
            cm.Create(f);

            try
            {
                cm.Create(f); // try to add the same customer again
                Assert.Fail("Added existing footcare to repository");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, customers.Count);
                Assert.AreEqual(f, cm.Read(1));
            }
        }

        /// <summary>
        /// Test method testing removal of an existing customer remove.
        /// </summary>

        [TestMethod]
        public void RemoveCustomerExistingCustomerTest()
        {
            IRepository<Customer> repo = mock.Object;
            CustomerManager cm = new CustomerManager(repo);

            Customer f = new Customer() { Id = 1, Firstname = "Kenny", Lastname = "kühl", Email = "Kuhlefar@gmail.com", PhoneNr = "329573402" };
            Customer ff = new Customer() { Id = 2, Firstname = "Sandy", Lastname = "Esko", Email = "Babe@gmail.com", PhoneNr = "112222222" };

            cm.Create(f);
            cm.Create(ff);

            cm.Delete(f);

            Assert.AreEqual(1, customers.Count);
            Assert.AreEqual(ff, cm.ReadAll()[0]);


        }

        /// <summary>
        /// Test method testing removal of a non-existing customer.
        /// Expects an ArgumentException to be thrown.
        /// </summary>

        [TestMethod]
        public void RemoveCustomerNonExistingCustomerTest()
        {
            IRepository<Customer> repo = mock.Object;
            CustomerManager cm = new CustomerManager(repo);

            Customer f = new Customer() { Id = 1, Firstname = "Kenny", Lastname = "kühl", Email = "Kuhlefar@gmail.com", PhoneNr = "329573402" };
            Customer ff = new Customer() { Id = 2, Firstname = "Sandy", Lastname = "Esko", Email = "Babe@gmail.com", PhoneNr = "112222222" };

            cm.Create(f);

            try
            {
                cm.Delete(ff);
                Assert.Fail("Removed none existing customer ");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, customers.Count);
                Assert.AreEqual(f, cm.ReadAll()[0]);
            }
        }
        /// <summary>
        /// Test method testing the retrieval of all customer from the repository.
        /// </summary>
        [TestMethod]
        public void ReadAllCustomer_Test()
        {
            IRepository<Customer> repo = mock.Object;
            CustomerManager cm = new CustomerManager(repo);

            Customer f = new Customer() { Id = 1, Firstname = "Kenny", Lastname = "kühl", Email = "Kuhlefar@gmail.com", PhoneNr = "329573402" };
            Customer ff = new Customer() { Id = 2, Firstname = "Sandy", Lastname = "Esko", Email = "Babe@gmail.com", PhoneNr = "112222222" };

            cm.Create(f);
            cm.Create(ff);

            IList<Customer> result = cm.ReadAll();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(f, result[0]);
            Assert.AreEqual(ff, result[1]);

        }
        /// <summary>
        /// Test method testing retrieval of an existing customer with a specific Id.
        /// </summary>

        [TestMethod]
        public void GetCustomerById_Existing_Customer_Test()
        {
            IRepository<Customer> repo = mock.Object;
            CustomerManager cm = new CustomerManager(repo);

            Customer f = new Customer() { Id = 1, Firstname = "Kenny", Lastname = "kühl", Email = "Kuhlefar@gmail.com", PhoneNr = "329573402" };
            Customer ff = new Customer() { Id = 2, Firstname = "Sandy", Lastname = "Esko", Email = "Babe@gmail.com", PhoneNr = "112222222" };

            cm.Create(f);
            cm.Create(ff);

            Customer result = cm.Read(1);

            Assert.AreEqual(f, result);
        }
        [TestMethod]
        public void UpdateexistingCustomer()
        {
            IRepository<Customer> repo = mock.Object;
            CustomerManager cm = new CustomerManager(repo);


            //Create and adds the customer
            Customer f1 = new Customer() { Id = 1, Firstname = "Kenny", Lastname = "kühl", Email = "Kuhlefar@gmail.com", PhoneNr = "329573402" };
            cm.Create(f1);
            Customer ff = new Customer() { Id = 1, Firstname = "Sandy", Lastname = "Esko", Email = "Babe@gmail.com", PhoneNr = "112222222" };
            
           

            bool isUpdated = cm.Update(ff);



            Assert.AreEqual(true, isUpdated);
            Assert.AreEqual(f1.Lastname, ff.Lastname);
            Assert.AreEqual(f1.Firstname, ff.Firstname);
            Assert.AreEqual(f1.Email, ff.Email);
            Assert.AreEqual(f1.PhoneNr, ff.PhoneNr);
            Assert.AreEqual(f1.Id, ff.Id);
        }
    }
}
