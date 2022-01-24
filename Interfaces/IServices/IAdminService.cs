using System.Collections.Generic;
using PlumbingService.DTOs;
using PlumbingService.Models.Entities;

namespace PlumbingService.Interfaces.IServices
{
    public interface IAdminService
    {
        bool Create(CreateAdminRequestModel model);

        bool Update(UpdateAdminRequestModel model, int id);

        void Delete(int id);

        AdminDTO Get(int id);

        IEnumerable<AdminDTO> GetAll();

        Admin GetByMail(string email);

        AdminDTO Login(LoginAdminRequestModel model);
    }
}