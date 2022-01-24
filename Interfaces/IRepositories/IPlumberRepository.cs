using System.Collections.Generic;
using PlumbingService.DTOs;
using PlumbingService.Models.Entities;

namespace PlumbingService.Interfaces.IRepositories
{
    public interface IPlumberRepository
    {
        bool Create(Plumber plumber);

        bool Update(Plumber plumber);

        void Delete(Plumber plumber);

        Plumber Get(int id);

        IEnumerable<PlumberDTO> GetAll();

        Plumber GetByMail(string email);
    }
}