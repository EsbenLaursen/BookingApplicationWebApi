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
using DLL.DAL.Repositories;

namespace BookingApplicationWebApi.Controllers
{
    public class FootCaresController : ApiController
    {
        private IRepository<FootCare> repo = new DllFacade().GetFootCareManager();

        // GET: api/FootCares
        public List<FootCare> GetFootCares()
        {
            return repo.ReadAll();
        }

        // GET: api/FootCares/5
        [ResponseType(typeof(FootCare))]
        public IHttpActionResult GetFootCare(int id)
        {
            FootCare footCare = repo.Read(id);
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

            repo.Update(footCare);

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

            repo.Create(footCare);

            return CreatedAtRoute("DefaultApi", new { id = footCare.Id }, footCare);
        }

        // DELETE: api/FootCares/5
        [ResponseType(typeof(FootCare))]
        public IHttpActionResult DeleteFootCare(int id)
        {
            FootCare footCare = repo.Read(id);
            if (footCare == null)
            {
                return NotFound();
            }

            repo.Delete(footCare);

            return Ok(footCare);
        }
    }
}