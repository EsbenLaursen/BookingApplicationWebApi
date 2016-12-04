using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DLL.DAL.Entities;

namespace DLL.DAL.Repositories
{
    public class ImageRepository : IRepository<Image>
    {
        public Image Create(Image t)
        {
            using (var ctx = new BookingDbContext())
            {
                ctx.Images.Add(t);
                ctx.SaveChanges();
                return t;
            }
        }

        public bool Delete(Image t)
        {
            using (var ctx = new BookingDbContext())
            {
                ctx.Images.Remove(t);
                ctx.SaveChanges();
                return true;
            }
        }

        public Image Read(int id)
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Images.FirstOrDefault(x => x.ImageId == id);
            }
        }

        public List<Image> ReadAll()
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Images.ToList();

            }
        }

        public bool Update(Image t)
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
