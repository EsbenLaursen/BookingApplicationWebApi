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
        List<Booking> Bookings;
        List<Room> Rooms;
        public CheckRoomAvailability()
        {
            Rooms = sg.ReadAll();
            Bookings = bg.ReadAll();
        }

        public List<Room> Check(DateTime start, DateTime end)
        {
            var dates = new List<DateTime>();
            for (var dt = start; dt <= end; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }

            List<Room> RoomsAvailable = new List<Room>();

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
            }

            return RoomsAvailable;
        }


        public List<DateTime> FetchUnavailableDates2()
        {
            List<DateTime> room1 = new List<DateTime>();
            List<DateTime> room2 = new List<DateTime>();
            List<DateTime> room3 = new List<DateTime>();

            List<DateTime> UnavailableDates = new List<DateTime>();

            foreach (var b in Bookings)
            {
                foreach (var r in b.Room)
                {
                    if (r.Id == 1)
                    {
                        for (var dt = b.StartDate; dt <= b.EndDate; dt = dt.AddDays(1))
                        {
                            room1.Add(dt);
                        }
                    }
                    else if (r.Id == 2)
                    {
                        for (var dt = b.StartDate; dt <= b.EndDate; dt = dt.AddDays(1))
                        {
                            room2.Add(dt);
                        }
                    }
                    else
                    {
                        for (var dt = b.StartDate; dt <= b.EndDate; dt = dt.AddDays(1))
                        {
                            room3.Add(dt);
                        }
                    }
                }
            }

            foreach (var d in room1)
            {
                if (room2.Any(x => x.Day == d.Day && x.Month == d.Month && x.Year == d.Year) && room3.Any(x => x.Day == d.Day && x.Month == d.Month && x.Year == d.Year))
                {
                    UnavailableDates.Add(d);
                }
            }
            return UnavailableDates;
        }



        public List<DateTime> FetchUnavailableDates()
        {
            var unavailabledates = new List<DateTime>();
            var bookeddates = new List<DateTime>();
            foreach (var b in Bookings)
            {
                if (b.EndDate.Millisecond >= DateTime.Now.Millisecond)
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
