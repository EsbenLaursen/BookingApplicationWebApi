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
    public class ReviewsController : ApiController
    {
        private IRepository<Review> repo = new DllFacade().GetReviewManager();

        // GET: api/Reviews
        public List<Review> GetReviews()
        {
            return repo.ReadAll();
        }

        // GET: api/Reviews/5
        [ResponseType(typeof(Review))]
        public IHttpActionResult GetReview(int id)
        {
            Review review = repo.Read(id);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // PUT: api/Reviews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReview(int id, Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != review.Id)
            {
                return BadRequest();
            }

            repo.Update(review);

           return StatusCode(HttpStatusCode.NoContent);
            
        }

        // POST: api/Reviews
        [ResponseType(typeof(Review))]
        public IHttpActionResult PostReview(Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Create(review);

            return CreatedAtRoute("DefaultApi", new { id = review.Id }, review);
        }

        // DELETE: api/Reviews/5
        [ResponseType(typeof(Review))]
        public IHttpActionResult DeleteReview(int id)
        {
            Review review = repo.Read(id);
            if (review == null)
            {
                return NotFound();
            }

            repo.Delete(review);

            return Ok(review);
        }
    }
}