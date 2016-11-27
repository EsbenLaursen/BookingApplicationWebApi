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

namespace BookingApplicationWebApi.Controllers
{
    public class FootCaresController : ApiController
    {
        private BookingDbContext db = new BookingDbContext();

        // GET: api/FootCares
        public IQueryable<FootCare> GetFootCares()
        {
            return db.FootCares;
        }

        // GET: api/FootCares/5
        [ResponseType(typeof(FootCare))]
        public IHttpActionResult GetFootCare(int id)
        {
            FootCare footCare = db.FootCares.Find(id);
            if (footCare == null)
            {
                return NotFound();
            }

            return Ok(footCare);
        }

        // PUT: api/FootCares/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFootCare(int id, FootCare footCare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != footCare.Id)
            {
                return BadRequest();
            }

            db.Entry(footCare).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FootCareExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/FootCares
        [ResponseType(typeof(FootCare))]
        public IHttpActionResult PostFootCare(FootCare footCare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FootCares.Add(footCare);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = footCare.Id }, footCare);
        }

        // DELETE: api/FootCares/5
        [ResponseType(typeof(FootCare))]
        public IHttpActionResult DeleteFootCare(int id)
        {
            FootCare footCare = db.FootCares.Find(id);
            if (footCare == null)
            {
                return NotFound();
            }

            db.FootCares.Remove(footCare);
            db.SaveChanges();

            return Ok(footCare);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FootCareExists(int id)
        {
            return db.FootCares.Count(e => e.Id == id) > 0;
        }
    }
}