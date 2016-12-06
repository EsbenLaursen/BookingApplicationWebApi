using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.DAL.Entities;
using DLL.DAL.Repositories;

namespace DLL.DAL.Managers
{
    public class ImageManager : IRepository<Image>
    {
        private IRepository<Image> ri;

        public ImageManager(IRepository<Image> repo)
        {
            if (repo == null)
            {
                throw new ArgumentNullException("Repository Missing");
            }

            ri = repo;
        }
        public Image Create(Image t)
        {
            if (ri.Read(t.ImageId) != null)
            {
                throw new ArgumentException("Image already exist");
            }
            return ri.Create(t);
        }

        public bool Delete(Image t)
        {
            if (ri.Read(t.ImageId) == null)
            {
                throw new ArgumentNullException("Image doesn't exist");
            }
            return ri.Delete(t);
        }


        public Image Read(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id out of range");
            }
            return ri.Read(id);
        }

        public List<Image> ReadAll()
        {
            return ri.ReadAll();
        }



        public bool Update(Image t)
        {
            if (t == null)
            {
                throw new ArgumentNullException("Image is null");
            }
            if (ri.Read(t.ImageId) == null)
            {
                throw new ArgumentNullException("Image isnt in DB");
            }
            Image i = ri.Read(t.ImageId);
            //i.ImageFileName = t.ImageFileName;

           return true;
        }

      
    }
}
