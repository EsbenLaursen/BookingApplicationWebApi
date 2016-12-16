using DLL.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DAL.Repositories
{
    public class AdminRepository
    {
        public Admin GetAdmin()
        {
            using (var context = new BookingDbContext())
            {
                return context.Admins.FirstOrDefault();
            }
        }
    }
}
