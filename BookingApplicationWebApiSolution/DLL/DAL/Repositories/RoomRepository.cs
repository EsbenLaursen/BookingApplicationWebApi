using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DLL.DAL.Entities;

namespace DLL.DAL.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        public Room Create(Room t)
        {
            using (var ctx = new BookingDbContext())
            {
                Room r = ctx.Rooms.Add(t);
                ctx.SaveChanges();
                return r;
            }
        }

        public bool Delete(Room t)
        {
            using (var ctx = new BookingDbContext())
            {
                ctx.Rooms.Attach(t);
                ctx.Rooms.Remove(t);
                ctx.SaveChanges();
                return true;
            }
        }

        public Room Read(int id)
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Rooms.Include(x => x.Bookings).FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Room> ReadAll()
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Rooms.Include(x=>x.Bookings).ToList();
            }
        }

        public bool Update(Room t)
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
