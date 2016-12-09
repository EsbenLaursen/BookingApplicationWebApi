using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.DAL.Entities;
using DLL.DAL.Repositories;

namespace DLL.DAL.Managers
{
    class TempBookingManager : IRepository<TemporaryBooking>
    {
        private IRepository<TemporaryBooking> tb;

        public TempBookingManager(IRepository<TemporaryBooking> repo)
        {
            if (repo == null)
            {
                throw new ArgumentNullException("No repository existing");
            }
            tb = repo;
        }

        public TemporaryBooking Create(TemporaryBooking t)
        {
            {
                if (tb.Read(t.Id) != null)
                {
                    throw new ArgumentException("TempBooking already exist");
                }
                if (t.Rooms == null || t.Rooms.Count <1)
                {
                    throw new ArgumentException("No rooms in temp booking");
                }
                return tb.Create(t);
            }
        }

        public bool Delete(TemporaryBooking t)
        {
            if (tb.Read(t.Id) == null)
            {
                throw new ArgumentNullException("TempBooking doesn't exist");
            }
            return tb.Delete(t);
        }

        public TemporaryBooking Read(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id out of range");
            }
            return tb.Read(id);

        }

        public List<TemporaryBooking> ReadAll()
        {
            return tb.ReadAll();
        }

        public bool Update(TemporaryBooking t)
        {

            if (tb.Read(t.Id) == null)
            {
                throw new ArgumentNullException();
            }
            TemporaryBooking tbook = tb.Read(t.Id);
            tbook.Rooms = t.Rooms;
            tbook.CustomerEmail = t.CustomerEmail;
            tbook.CustomerLastname = t.CustomerLastname;
            tbook.CustomerFirstname = t.CustomerFirstname;
            tbook.CustomerPhoneNr = t.CustomerPhoneNr;
            tbook.EndDate = t.EndDate;
            tbook.StartDate = t.StartDate;


            return true;
        }
    }
}
