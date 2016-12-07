using DLL.DAL.Entities;
using DLL.DAL.Repositories;

namespace DLL.DAL
{
   public class DllFacade
    {
        public IRepository<TemporaryBooking> GetTempBookingManager()
        {
            return new TempBookingRepository();
        }
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
