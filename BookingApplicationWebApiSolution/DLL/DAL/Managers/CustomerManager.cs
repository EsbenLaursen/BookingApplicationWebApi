using DLL.DAL.Entities;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DAL.Managers
{
    public class CustomerManager : IRepository<Customer>
    {
        IRepository<Customer> customerRepo;
        public CustomerManager(IRepository<Customer> repo)
        {
            if(repo == null)
            {
                throw new ArgumentNullException("Repository is null");
            }
            customerRepo = repo;
        }

        public Customer Create(Customer t)
        {
            if (customerRepo.Read(t.Id) != null)
            {
                throw new ArgumentException("Customer already exist");
            }
            return customerRepo.Create(t);
        }

        public bool Delete(Customer t)
        {
            if (customerRepo.Read(t.Id) == null)
            {
                throw new ArgumentNullException("Customer doesn't exist");
            }
            return customerRepo.Delete(t);
        }

        public Customer Read(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id out of range");
            }
            return customerRepo.Read(id);
        }

        public List<Customer> ReadAll()
        {

            return customerRepo.ReadAll();
        }

        public bool Update(Customer updatedCustomer)
        {
            if (customerRepo.Read(updatedCustomer.Id) == null)
            {
                throw new ArgumentNullException("Customer not found");
            }
            Customer c= customerRepo.Read(updatedCustomer.Id);
            c.Email = updatedCustomer.Email;
            c.Lastname = updatedCustomer.Lastname;
            c.PhoneNr = updatedCustomer.PhoneNr;
            c.Firstname = updatedCustomer.Firstname;

            return true;
        }
    }
}
