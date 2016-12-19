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
using DLL.DAL;
using DLL.DAL.Entities;
using DLL.DAL.Repositories;

namespace BookingApplicationWebApi.Controllers
{
    public class TemporaryBookingsController : ApiController
    {
        private IRepository<TemporaryBooking> repo = new DllFacade().GetTempBookingManager();

        // GET: api/TemporaryBookings
        [ResponseType(typeof(List<TemporaryBooking>))]
        public IHttpActionResult GetTempBookings()
        {
            return Ok(repo.ReadAll());
        }

        // GET: api/TemporaryBookings/5
        [ResponseType(typeof(TemporaryBooking))]
        public IHttpActionResult GetTemporaryBooking(int id)
        {
            TemporaryBooking temporaryBooking = repo.Read(id);
            if (temporaryBooking == null)
            {
                return NotFound();
            }

            return Ok(temporaryBooking);
        }
        [Authorize]
        // PUT: api/TemporaryBookings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTemporaryBooking(int id, TemporaryBooking temporaryBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != temporaryBooking.Id)
            {
                return BadRequest();
            }
            repo.Update(temporaryBooking);

           
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Authorize]
        // POST: api/TemporaryBookings
        [ResponseType(typeof(TemporaryBooking))]
        public IHttpActionResult PostTemporaryBooking(TemporaryBooking temporaryBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Create(temporaryBooking);
           
            return CreatedAtRoute("DefaultApi", new { id = temporaryBooking.Id }, temporaryBooking);
        }
        [Authorize]
        // DELETE: api/TemporaryBookings/5
        [ResponseType(typeof(TemporaryBooking))]
        public IHttpActionResult DeleteTemporaryBooking(int id)
        {
            TemporaryBooking temporaryBooking = repo.Read(id);
            if (temporaryBooking == null)
            {
                return NotFound();
            }

            repo.Delete(temporaryBooking);

            return Ok(temporaryBooking);
        }
    }
}