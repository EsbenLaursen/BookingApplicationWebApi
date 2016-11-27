using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.DAL.Entities;
using DLL.DAL;
using System.Data.Entity;

namespace DLL.Repositories
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
                ctx.Rooms.Remove(t);
                ctx.SaveChanges();
                return true;
            }
        }

        public Room Read(int id)
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Rooms.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Room> ReadAll()
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Rooms.ToList();
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
