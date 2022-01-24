using System.Collections.Generic;
using System.Linq;
using PlumbingService.Context;
using PlumbingService.DTOs;
using PlumbingService.Interfaces.IRepositories;
using PlumbingService.Models.Entities;

namespace PlumbingService.Implementation.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ContextApp _contextApp;

        public AdminRepository(ContextApp contextApp)
        {
            _contextApp = contextApp;
        }
        public bool Create(Admin admin)
        {
            _contextApp.Admins.Add(admin);
            _contextApp.SaveChanges();
            return true;
        }

        public void Delete(Admin admin)
        {
            _contextApp.Admins.Remove(admin);
            _contextApp.SaveChanges();
        }

        public Admin Get(int id)
        {
            return _contextApp.Admins.Find(id);
        }

        public IEnumerable<AdminDTO> GetAll()
        {
            return _contextApp.Admins.ToList().Select(admin => new AdminDTO 
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                AdminPhoto = admin.AdminPhoto,
                PhoneNumber = admin.PhoneNumber
            });
        }

        public Admin GetByMail(string email)
        {
            return _contextApp.Admins.SingleOrDefault(admin => admin.Email == email);
        }

        public bool Update(Admin admin)
        {
            _contextApp.Admins.Update(admin);
            _contextApp.SaveChanges();
            return true;
        }
    }
}