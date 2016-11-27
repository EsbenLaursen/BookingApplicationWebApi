using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApplicationWebApi;
using DLL.DAL.Entities;
using DLL.DAL.Managers;
using DLL.Repositories;
using DLL.DAL.Repositories;

namespace DLL
{
   public class DllFacade
    {
        public IRepository<Room> GetRoomManager()
        {
            return new RoomRepository();
        }

        public IRepository<Customer> GetCustomerManager()       
        {
            return new CustomerRepository();
        }

        public IRepository<FootCare> GetFootCareManager()
        {
            return new FootCareRepository();
        }

        public IRepository<Booking> GetBookingManager()
        {
            return new BookingRepository();
        }

        public IRepository<Image> GetImageManager()
        {
            return new ImageRepository();
        }
    }

}
