﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BookingApplicationWebApi;
using DLL.DAL;
using DLL;
using DLL.DAL.Entities;
using DLL.DAL.Repositories;

namespace BookingApplicationWebApi.Controllers
{
    [Authorize]
    public class BookingsController : ApiController
    {

        public IRepository<Booking> repo = new DllFacade().GetBookingManager();

        [AllowAnonymous]
        // GET: api/Bookings
        public List<Booking> GetBookings()
        {
            return repo.ReadAll();
        }

        [AllowAnonymous]
        // GET: api/Bookings/5
        [ResponseType(typeof(Booking))]
        public IHttpActionResult GetBooking(int id)
        {
            Booking booking = repo.Read(id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        [Authorize]
        // PUT: api/Bookings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBooking(int id, Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != booking.CustomerId)
            {
                return BadRequest();
            }

            repo.Update(booking);

            return StatusCode(HttpStatusCode.NoContent);
        }
        [Authorize]
        // POST: api/Bookings
        [ResponseType(typeof(Booking))]
        public IHttpActionResult PostBooking(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Create(booking);

            return CreatedAtRoute("DefaultApi", new { id = booking.CustomerId }, booking);
        }

        // DELETE: api/Bookings/5
        [Authorize]
        [ResponseType(typeof(Booking))]
        public IHttpActionResult DeleteBooking(int id)
        {
            Booking booking = repo.Read(id);
            if (booking == null)
            {
                return NotFound();
            }

            repo.Delete(booking);

            return Ok(booking);
        }

    }
}