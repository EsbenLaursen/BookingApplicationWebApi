using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.DAL.Entities;

namespace DLL.DAL.Repositories
{
    class TempBookingRepository : IRepository<TemporaryBooking>
    {
        public TemporaryBooking Create(TemporaryBooking t)
        {
            using (var ctx = new BookingDbContext())
            {
                TemporaryBooking r = ctx.TempBookings.Add(t);
                ctx.SaveChanges();
                return r;
            }
        }

        public bool Delete(TemporaryBooking t)
        {
            using (var ctx = new BookingDbContext())
            {
                ctx.TempBookings.Remove(t);
                ctx.SaveChanges();
                return true;
            }
        }

        public TemporaryBooking Read(int id)
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.TempBookings.FirstOrDefault(x=> x.Id == id);
            }
        }

        public List<TemporaryBooking> ReadAll()
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.TempBookings.ToList();
            }
        }

        public bool Update(TemporaryBooking t)
        {
            using (var ctx = new BookingDbContext())
            {
                ctx.Entry(t).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
        }
    }
}
