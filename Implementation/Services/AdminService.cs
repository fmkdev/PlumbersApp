using System.Collections.Generic;
using PlumbingService.DTOs;
using PlumbingService.Implementation.Repositories;
using PlumbingService.Interfaces.IRepositories;
using PlumbingService.Interfaces.IServices;
using PlumbingService.Models.Entities;

namespace PlumbingService.Repositories.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public bool Create(CreateAdminRequestModel model)
        {
            var admin = new Admin
            {
                AdminPhoto = model.AdminPhoto,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber
            };
            return _adminRepository.Create(admin);
        }

        public void Delete(int id)
        {
            var admin = _adminRepository.Get(id);
            if(admin == null)
            {
                throw new System.Exception("Admin with the Id not found");
            }
            _adminRepository.Delete(admin);
        }

        public AdminDTO Get(int id)
        {
            var admin = _adminRepository.Get(id);
            if(admin == null)
            {
                throw new System.Exception("Admin with the Id not found");
            }
            return new AdminDTO
            {
                AdminPhoto = admin.AdminPhoto,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                Id = admin.Id,
                PhoneNumber = admin.PhoneNumber
            };
        }

        public IEnumerable<AdminDTO> GetAll()
        {
            return _adminRepository.GetAll();
        }

        public Admin GetByMail(string email)
        {
            return _adminRepository.GetByMail(email);
        }

        public AdminDTO Login(LoginAdminRequestModel model)
        {
            var admin = _adminRepository.GetByMail(model.Email);
            if(admin == null || admin.Password != model.Password)
            {
                return null;
            }
            return new AdminDTO
            {
                AdminPhoto = admin.AdminPhoto,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                Id = admin.Id,
                PhoneNumber = admin.PhoneNumber
            };
        }

        public bool Update(UpdateAdminRequestModel model, int id)
        {
            var admin = _adminRepository.Get(id);
            if(admin == null)
            {
                throw new System.Exception("Admin with the Id not found");
            }
            admin.FirstName = model.FirstName;
            admin.LastName = model.LastName;
            admin.Email = model.Email;
            admin.AdminPhoto = model.AdminPhoto;
            admin.PhoneNumber = model.PhoneNumber;

            return _adminRepository.Update(admin);
        }
    }
}