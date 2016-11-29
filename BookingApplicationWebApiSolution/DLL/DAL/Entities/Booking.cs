using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DLL.DAL.Entities;

namespace BookingApplicationWebApi
{
    [Serializable]
    public class Booking
    {
        public int Id { get; set; }
        
        public int CustomerId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual List<Room> Room { get; set; }
        public bool Breakfast { get; set; }

        public virtual Customer Customer { get; set; }
    }
}