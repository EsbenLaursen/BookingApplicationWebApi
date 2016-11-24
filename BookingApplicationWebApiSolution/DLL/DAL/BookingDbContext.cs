using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DAL
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext() : base("BookingAppDB")
        {
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<BookingDbContext>(new BookingDbInitializer());
        }
    }

}
