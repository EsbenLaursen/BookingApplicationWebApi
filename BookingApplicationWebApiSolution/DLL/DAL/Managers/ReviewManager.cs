using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.DAL.Entities;
using DLL.DAL.Repositories;

namespace DLL.DAL.Managers
{
    public class ReviewManager : IRepository<Review>
    {
        private IRepository<Review> fcr;

        public ReviewManager(IRepository<Review> repo)
        {
            if (repo == null)
            {
                throw new ArgumentNullException("No repository existing");
            }
            fcr = repo;
        }

        public Review Create(Review t)
        {
            if (fcr.Read(t.Id) != null)
            {
                throw new ArgumentException("Review already exist");
            }
            return fcr.Create(t);
        }

        public bool Delete(Review t)
        {
            if (fcr.Read(t.Id) == null)
            {
                throw new ArgumentNullException("Review doesn't exist");
            }
            return fcr.Delete(t);
        }

        public Review Read(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id out of range");
            }
            return fcr.Read(id);

        }

        public List<Review> ReadAll()
        {
            return fcr.ReadAll();
        }

        public bool Update(Review r)
        {
            if (fcr.Read(r.Id) == null)
            {
                throw new ArgumentNullException();
            }
            Review rw = fcr.Read(r.Id);
            rw.Name = r.Name;
            
            rw.Description = r.Description;

            return true;
        }
    }


}
