using System.Collections.Generic;
using PlumbingService.DTOs;
using PlumbingService.Models.Entities;

namespace PlumbingService.Interfaces.IServices
{
    public interface ICustomerService
    {
         bool Create(CreateCustomerRequestModel model);

        bool Update(UpdateCustomerRequestModel model, int id);

        void Delete(int id);

        CustomerDTO Get(int id);

        IEnumerable<CustomerDTO> GetAll();

        CustomerDTO GetByMail(string email);

        CustomerDTO Login(LoginCustomerRequestModel model);
    }
}