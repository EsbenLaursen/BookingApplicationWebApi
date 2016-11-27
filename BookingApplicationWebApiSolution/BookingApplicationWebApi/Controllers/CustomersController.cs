using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DLL;
using DLL.DAL;
using DLL.DAL.Entities;
using DLL.Repositories;

namespace BookingApplicationWebApi.Controllers
{
    public class CustomersController : ApiController
    {
        private IRepository<Customer> repo = new DllFacade().GetCustomerManager();

        // GET: api/Customers
        public List<Customer> GetCustomers()
        {
            return repo.ReadAll();
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = repo.Read(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            repo.Update(customer);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Create(customer);

            return CreatedAtRoute("DefaultApi", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = repo.Read(id);
            if (customer == null)
            {
                return NotFound();
            }

            repo.Delete(customer);

            return Ok(customer);
        }
    }
}