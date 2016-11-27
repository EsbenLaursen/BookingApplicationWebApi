using BookingApplicationWebApi;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DAL.Repositories
{
    public class BookingRepository : IRepository<Booking>
    {
        public Booking Create(Booking t)
        {
            using (var ctx = new BookingDbContext())
            {
                Booking r = ctx.Bookings.Add(t);
                ctx.SaveChanges();
                return r;
            }
        }

        public bool Delete(Booking t)
        {
            using (var ctx = new BookingDbContext())
            {
                ctx.Bookings.Remove(t);
                ctx.SaveChanges();
                return true;
            }
        }

        public Booking Read(int id)
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Bookings.Include(x => x.Customer).Include(x => x.Room).FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Booking> ReadAll()
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Bookings.Include(x => x.Customer).Include(x => x.Room).ToList();
            }
        }

        public bool Update(Booking t)
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
