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
using CloudinaryDotNet;
using DLL;
using DLL.DAL;
using DLL.DAL.Entities;
using DLL.DAL.Repositories;

namespace BookingApplicationWebApi.Controllers
{
    public class ImagesController : ApiController
    {
        private IRepository<Image> repo = new DllFacade().GetImageManager();

        
        // GET: api/Images
        public List<Image> GetImages()
        {
            return repo.ReadAll();
        }

        // GET: api/Images/5
        [ResponseType(typeof(Image))]
        public IHttpActionResult GetImage(int id)
        {
            Image image = repo.Read(id);
            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }

        [Authorize]
        // PUT: api/Images/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutImage(int id, Image image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != image.ImageId)
            {
                return BadRequest();
            }

            repo.Update(image);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Authorize]
        // POST: api/Images
        [ResponseType(typeof(Image))]
        public IHttpActionResult PostImage(Image image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Create(image);

            return CreatedAtRoute("DefaultApi", new { id = image.ImageId }, image);
        }
        [Authorize]
        // DELETE: api/Images/5
        [ResponseType(typeof(Image))]
        public IHttpActionResult DeleteImage(int id)
        {
            Image image = repo.Read(id);
            if (image == null)
            {
                return NotFound();
            }

            repo.Delete(image);

            return Ok(image);
        }
    }
}