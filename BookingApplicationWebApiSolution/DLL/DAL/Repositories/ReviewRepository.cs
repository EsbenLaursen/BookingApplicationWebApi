using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DLL.DAL.Entities;

namespace DLL.DAL.Repositories
{
    public class ReviewRepository : IRepository<Review>
    {
        public Review Create(Review t)
        {
            using (var ctx = new BookingDbContext())
            {
                ctx.Reviews.Add(t);
                ctx.SaveChanges();
                return t;
            }
        }

        public bool Delete(Review t)
        {
            using (var ctx = new BookingDbContext())
            {
                ctx.Reviews.Remove(t);
                ctx.SaveChanges();
                return true;
            }
        }

        public Review Read(int id)
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Reviews.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Review> ReadAll()
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Reviews.ToList();
            }
        }

        public bool Update(Review t)
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
