using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.DAL.Entities;
using DLL.DAL.Repositories;

namespace DLL.DAL.Managers
{
    public class BookingManager : IRepository<Booking>
    {
        IRepository<Booking> bookingRepo;
        public BookingManager(IRepository<Booking> repo)
        {
            if (repo == null)
            {
                throw new ArgumentNullException("Repository is null");
            }
            bookingRepo = repo;
        }

        public Booking Create(Booking t)
        {
            if (bookingRepo.Read(t.Id) != null)
            {
                throw new ArgumentException("Booking already exist");
            }
            if (t.Room == null || t.Room.Count < 1)
            {
                throw new ArgumentException("No rooms in booking");
            }
            return bookingRepo.Create(t);
        }

        public bool Delete(Booking t)
        {

            if (bookingRepo.Read(t.Id) == null)
            {
                throw new ArgumentNullException("Booking doesn't exist");
            }
            return bookingRepo.Delete(t);
        }

        public Booking Read(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id out of range");
            }
            return bookingRepo.Read(id);
        }

        public List<Booking> ReadAll()
        {
            return bookingRepo.ReadAll();
        }

        public bool Update(Booking updatedBooking)
        {
            if (bookingRepo.Read(updatedBooking.Id) == null)
            {
                throw new ArgumentNullException("booking not found");
            }
            Booking b = bookingRepo.Read(updatedBooking.Id);
            b.StartDate = updatedBooking.StartDate;
            b.EndDate = updatedBooking.EndDate;
            b.Breakfast = updatedBooking.Breakfast;
            
            return true;
        }
    }
}
