using System.Collections.Generic;
using PlumbingService.DTOs;
using PlumbingService.Models.Entities;

namespace PlumbingService.Interfaces.IRepositories
{
    public interface IAdminRepository
    {
        bool Create(Admin admin);

        bool Update(Admin admin);

        void Delete(Admin admin);

        Admin Get(int id);

        IEnumerable<AdminDTO> GetAll();

        Admin GetByMail(string email);
    }
}