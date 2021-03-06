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
using DLL.DAL;
using DLL.DAL.Entities;
using DLL;
using DLL.DAL.Repositories;

namespace BookingApplicationWebApi.Controllers
{
    public class RoomsController : ApiController
    {
        //  private BookingDbContext db = new BookingDbContext();

        IRepository<Room> repo = new DllFacade().GetRoomManager();

        // GET: api/Rooms
        public List<Room> GetRooms()
        {
            return repo.ReadAll();
        }

        // GET: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult GetRoom(int id)
        {
            Room room = repo.Read(id) ;
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        [Authorize]
        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoom(int id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.Id)
            {
                return BadRequest();
            }

            repo.Update(room);

            return StatusCode(HttpStatusCode.NoContent);
        }
        [Authorize]
        // POST: api/Rooms
        [ResponseType(typeof(Room))]
        public IHttpActionResult PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Create(room);

            return CreatedAtRoute("DefaultApi", new { id = room.Id }, room);
        }
        [Authorize]
        // DELETE: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult DeleteRoom(int id)
        {
            Room room = repo.Read(id);
            if (room == null)
            {
                return NotFound();
            }

            repo.Delete(room);

            return Ok(room);
        }

    }
}