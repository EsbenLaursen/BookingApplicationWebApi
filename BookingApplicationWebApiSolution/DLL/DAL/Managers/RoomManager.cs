using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.DAL.Entities;
using DLL.Repositories;

namespace DLL.DAL.Managers
{
    public class RoomManager : IRepository<Room>

        
    {
        
        private IRepository<Room> rr;

        public RoomManager(IRepository<Room> repo)
        {
            if (repo == null)
            {
                throw new ArgumentNullException("Repository is missing");
            }
            rr = repo;
        }
        public Room Create(Room t)
        {
            if (Read(t) != null)
            {
                throw new ArgumentNullException("Room already exists");
            }
            
            return rr.Create(t);
        }

        public bool Delete(Room t)
        {
            if (t.Id <= 0)
            {
                throw new ArgumentOutOfRangeException("Room does not exist");
            }
            return rr.Delete(t);
        }

        public Room Read(Room t)
        {
            if (t.Id  <= 0)
            {
                throw new ArgumentOutOfRangeException("Id out of range");
            }
            return rr.Read(t);
        }

        public List<Room> ReadAll()
        {
            return rr.ReadAll();
        }

        public bool Update(Room t)
        {
            throw new NotImplementedException();
        }
    }
}
