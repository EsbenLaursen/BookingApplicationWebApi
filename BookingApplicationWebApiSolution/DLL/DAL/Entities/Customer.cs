using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApplicationWebApi;

namespace DLL.DAL.Entities
{
    [Serializable]
    public class Customer
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNr { get; set; }


        public virtual Booking Booking { get; set; }
    }
}
