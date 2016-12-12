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

namespace BookingApplicationWebApi.Controllers
{
    public class CalendarController : ApiController
    {
        IRepository<Booking> br = new DllFacade().GetBookingManager();
        [HttpGet]
        public List<DateTime> GetAvailableDates()
        {
            List<Booking> Bookings = br.ReadAll();
            CheckRoomAvailability check = new CheckRoomAvailability();
            List<DateTime> dates = check.FetchUnavailableDates2();
            return dates;
        }
        [HttpGet]
        public List<Room> GetRooms(DateTime to, DateTime from)
        {
            CheckRoomAvailability check = new CheckRoomAvailability();
            List<Room> rooms = check.Check(to, from);
            return rooms;
        }


    }
}
