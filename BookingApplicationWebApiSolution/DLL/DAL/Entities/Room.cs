using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApplicationWebApi;

namespace DLL.DAL.Entities
{
    [Serializable]
    public class Room 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Persons { get; set; }

        public virtual Booking Booking { get; set; }
    }
}
