using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.DAL.Entities;
using DLL.DAL.Repositories;

namespace DLL.DAL.Managers
{
    public class FootCareManager : IRepository<FootCare>
    {
        private IRepository<FootCare> fcr;

        public FootCareManager(IRepository<FootCare> repo)
        {
            if (repo == null)
            {
                throw new ArgumentNullException("No repository existing");
            }
            fcr = repo;
        }

        public FootCare Create(FootCare t)
        {
            if (fcr.Read(t.Id) != null)
            {
                throw new ArgumentException("Footcare already exist");
            }
           return fcr.Create(t);
        }

        public bool Delete(FootCare t)
        {
            if (fcr.Read(t.Id) == null)
            {
                throw new ArgumentNullException("Footcare doesn't exist");
            }
            return fcr.Delete(t);
        }

        public FootCare Read(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id out of range");
            }
           return fcr.Read(id);

        }

        public List<FootCare> ReadAll()
        {
            return fcr.ReadAll();
        }

        public bool Update(FootCare t)
        {
            if (fcr.Read(t.Id) == null)
            {
                throw new ArgumentNullException();
            }
            FootCare fc = fcr.Read(t.Id);
            fc.Name = t.Name;
            fc.Description = t.Description;
            fc.Price = t.Price;
            fc.Duration = t.Duration;
            
            return true;
        }
    }
}
