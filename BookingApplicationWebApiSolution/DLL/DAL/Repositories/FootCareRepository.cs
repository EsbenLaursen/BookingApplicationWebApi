using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DLL.DAL.Entities;

namespace DLL.DAL.Repositories
{
    public class FootCareRepository : IRepository<FootCare>
    {
        public FootCare Create(FootCare t)
        {
            using (var ctx = new BookingDbContext())
            {
                ctx.FootCares.Add(t);
                ctx.SaveChanges();
                return t;
            }
        }

        public bool Delete(FootCare t)
        {
            using (var ctx = new BookingDbContext())
            {
                ctx.FootCares.Attach(t);
                ctx.FootCares.Remove(t);
                ctx.SaveChanges();
                return true;
            }
        }

        public FootCare Read(int id)
        {
            using (var ctx = new BookingDbContext())
            {
              return ctx.FootCares.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<FootCare> ReadAll()
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.FootCares.ToList();
            }
        }

        public bool Update(FootCare t)
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
