using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.DAL.Entities;
using DLL.DAL.Repositories;

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
            if (Read(t.Id) != null)
            {
                throw new ArgumentNullException("Room already exists");
            }

            return rr.Create(t);
        }

        public bool Delete(Room t)
        {
            if (rr.Read(t.Id) == null)
            {
                throw new ArgumentOutOfRangeException("Room does not exist");
            }
            return rr.Delete(t);
        }

        public Room Read(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException("Id out of range");
            }
            return rr.Read(id);
        }

        public List<Room> ReadAll()
        {
            return rr.ReadAll();
        }

        public bool Update(Room t)
        {
            if (t == null)
            {
                throw new ArgumentNullException("Room is null");
            }
            if (rr.Read(t.Id) == null)
            {
                throw new ArgumentNullException("Room isnt in DB");
            }
            Room r = rr.Read(t.Id);
            r.Name = t.Name;
            r.Description = t.Description;
            r.Price = t.Price;
            r.Persons = t.Persons;

            return true;

        }
    }
}
