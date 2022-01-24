using System.Collections.Generic;
using PlumbingService.DTOs;
using PlumbingService.Models.Entities;

namespace PlumbingService.Interfaces.IRepositories
{
    public interface ICustomerRepository
    {
        bool Create(Customer customer);

        bool Update(Customer customer);

        void Delete(Customer customer);

        Customer Get(int id);

        IEnumerable<CustomerDTO> GetAll();

        Customer GetByMail(string email);
    }
}