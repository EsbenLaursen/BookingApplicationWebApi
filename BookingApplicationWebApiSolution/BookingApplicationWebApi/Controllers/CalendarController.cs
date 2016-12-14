using DLL;
using DLL.DAL;
using DLL.DAL.Entities;
using DLL.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DLL.DAL.Managers;

namespace BookingApplicationWebApi.Controllers
{
    public class CalendarController : ApiController
    {
        IRepository<Booking> bm = new DllFacade().GetBookingManager();
        IRepository<Room> rm = new DllFacade().GetRoomManager();
        [HttpGet]
        public List<DateTime> GetAvailableDates()
        {
            CheckRoomAvailability check = new CheckRoomAvailability(rm.ReadAll(), bm.ReadAll());
            List<DateTime> dates = check.FetchUnavailableDates();
            return dates;
        }
        [HttpGet]
        public List<Room> GetRooms(DateTime to, DateTime from)
        {
            CheckRoomAvailability check = new CheckRoomAvailability(rm.ReadAll(), bm.ReadAll());
            List<Room> rooms = check.Check(to, from);
            return rooms;
        }


    }
}
