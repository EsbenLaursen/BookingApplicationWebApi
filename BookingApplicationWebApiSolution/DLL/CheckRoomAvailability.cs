using DLL.DAL;
using DLL.DAL.Entities;
using DLL.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DLL
{

    public class CheckRoomAvailability
    {
        public IRepository<Room> sg = new DllFacade().GetRoomManager();
        public IRepository<Booking> bg = new DllFacade().GetBookingManager();
        public List<Booking> Bookings;
        public List<Room> Rooms;
        public CheckRoomAvailability()
        {
            //Rooms = sg.ReadAll();
            //Bookings = bg.ReadAll();
        }

        public List<Room> Check(DateTime start, DateTime end)
        {
            List<Room> RoomsAvailable = new List<Room>();
            if (start.Date >= DateTime.Now.Date)
            {
                
            
            var dates = new List<DateTime>();
            for (var dt = start; dt <= end; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }

            

            foreach (var r1 in Rooms)//3rooms
            {
                bool succes = true;
                foreach (var b in Bookings)//all bookings 
                {
                    foreach (var r in b.Room)
                    {
                        if (r.Name == r1.Name)
                        {
                            for (var dt = b.StartDate; dt <= b.EndDate; dt = dt.AddDays(1))
                            {
                                if (dates.Any(x => x.Day == dt.Day && x.Month == dt.Month && x.Year == dt.Year))
                                {
                                    succes = false;
                                }
                            }
                        }
                    }
                }
                if (succes)
                {
                    RoomsAvailable.Add(r1);
                }
            }}

            return RoomsAvailable;
        }


        public List<DateTime> FetchUnavailableDates()
        {
            var unavailabledates = new List<DateTime>();
            var bookeddates = new List<DateTime>();
            foreach (var b in Bookings)
            {
                if (DateTime.Compare(b.EndDate, DateTime.Now)>=0)
                {
                    for (var dt = b.StartDate; dt <= b.EndDate; dt = dt.AddDays(1))
                    {
                        bookeddates.Add(dt);
                    }
                }

            }
            
            foreach (var undate in bookeddates)
            {
                int i = 0;
                foreach (var r in Rooms)
                {
                    foreach (var booking in r.Bookings)
                    {
                        for (var date = booking.StartDate; date <= booking.EndDate; date = date.AddDays(1))
                        {
                            if (date.Month == undate.Month && date.Date == undate.Date && date.Year == undate.Year)
                            {
                                i++;
                            }
                        }
                    }
                }
                if (i == Rooms.Count)
                {
                    unavailabledates.Add(undate);
                }
            }

            return unavailabledates;
        }
    }
}
