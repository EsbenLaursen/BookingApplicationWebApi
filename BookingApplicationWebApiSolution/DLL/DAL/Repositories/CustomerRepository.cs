using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DLL.DAL.Entities;

namespace DLL.DAL.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        public Customer Create(Customer t)
        {
            using (var ctx = new BookingDbContext())
            {
                Customer c = ctx.Customers.Add(t);
                ctx.SaveChanges();
                return c;
            }
        }

        public bool Delete(Customer t)
        {
            using (var ctx = new BookingDbContext())
            {
                Customer c = ctx.Customers.Remove(t);
                ctx.SaveChanges();
                return true;
            }
        }

        public Customer Read(int id)
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Customers.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Customer> ReadAll()
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Customers.ToList();
            }
        }

        public bool Update(Customer t)
        {
            using (var ctx = new BookingDbContext())
            {
                ctx.Entry(t).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
        }
    }
}
