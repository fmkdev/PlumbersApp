using System.Collections.Generic;
using System.Linq;
using PlumbingService.Context;
using PlumbingService.DTOs;
using PlumbingService.Enums;
using PlumbingService.Interfaces.IRepositories;
using PlumbingService.Models.Entities;

namespace PlumbingService.Implementation.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ContextApp _contextApp;

        public CustomerRepository(ContextApp contextApp)
        {
            _contextApp = contextApp;
        }
        public bool Create(Customer customer)
        {
            _contextApp.Customers.Add(customer);
            _contextApp.SaveChanges();
            return true;
        }

        public bool CreateJob(Job job)
        {
            _contextApp.Jobs.Add(job);
            _contextApp.SaveChanges();
            return true;
        }

        public void Delete(Customer customer)
        {
            _contextApp.Customers.Remove(customer);
            _contextApp.SaveChanges();
        }

        public Customer Get(int id)
        {
            return _contextApp.Customers.Find(id);
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            return _contextApp.Customers.ToList().Select(admin => new CustomerDTO 
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                CustomerPhoto = admin.CustomerPhoto,
                PhoneNumber = admin.PhoneNumber
            });
        }

        public Customer GetByMail(string email)
        {
            return _contextApp.Customers.SingleOrDefault(customer => customer.Email == email);
        }

        public bool Update(Customer customer)
        {
            _contextApp.Customers.Update(customer);
            _contextApp.SaveChanges();
            return true;
        }
    }
}