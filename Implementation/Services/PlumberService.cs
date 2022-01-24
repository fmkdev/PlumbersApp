using System.Collections.Generic;
using PlumbingService.DTOs;
using PlumbingService.Interfaces.IRepositories;
using PlumbingService.Interfaces.IServices;
using PlumbingService.Models.Entities;

namespace PlumbingService.Implementation.Services
{
    public class PlumberService : IPlumberService
    {
        private readonly IPlumberRepository _plumberRepository;

        public PlumberService(IPlumberRepository plumberRepository)
        {
            _plumberRepository = plumberRepository;
        }
        public bool Create(CreatePlumberRequestModel model)
        {
            var plumber = new Plumber
            {
                PlumberPhoto = model.PlumberPhoto,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };
            return _plumberRepository.Create(plumber);
        }

        public void Delete(int id)
        {
             var plumber = _plumberRepository.Get(id);
            if (plumber == null)
            {
                throw new System.Exception("Plumber with the Id not found");
            }
            _plumberRepository.Delete(plumber);
        }

        public PlumberDTO Get(int id)
        {
            var plumber = _plumberRepository.Get(id);
            if (plumber == null)
            {
                throw new System.Exception("Plumber with the Id not found");
            }
            return new PlumberDTO
            {
                PlumberPhoto = plumber.PlumberPhoto,
                FirstName = plumber.FirstName,
                LastName = plumber.LastName,
                Email = plumber.Email,
                Password = plumber.Password,
                PhoneNumber = plumber.PhoneNumber,
                Address = plumber.Address
            };
        }

        public IEnumerable<PlumberDTO> GetAll()
        {
            return _plumberRepository.GetAll();
        }

        public PlumberDTO GetByMail(string email)
        {
            var plumber = _plumberRepository.GetByMail(email);
            if (plumber == null)
            {
                throw new System.Exception("Plumber with the Email not found");
            }
            return new PlumberDTO
            {
                PlumberPhoto = plumber.PlumberPhoto,
                FirstName = plumber.FirstName,
                LastName = plumber.LastName,
                Email = plumber.Email,
                Password = plumber.Password,
                PhoneNumber = plumber.PhoneNumber
            };
        }

        public bool Update(UpdatePlumberRequestModel model, int id)
        {
            var plumber = _plumberRepository.Get(id);
            if (plumber == null)
            {
                throw new System.Exception("Plumber with the Id not found");
            }
            plumber.FirstName = model.FirstName;
            plumber.LastName = model.LastName;
            plumber.Email = model.Email;
            plumber.PlumberPhoto = model.PlumberPhoto;
            plumber.PhoneNumber = model.PhoneNumber;

            return _plumberRepository.Update(plumber);
        }

         public PlumberDTO Login(LoginPlumberRequestModel model)
        {
            var plumber = _plumberRepository.GetByMail(model.Email);
            if(plumber == null || plumber.Password != model.Password)
            {
                return null;
            }
            return new PlumberDTO
            {
                PlumberPhoto = plumber.PlumberPhoto,
                FirstName = plumber.FirstName,
                LastName = plumber.LastName,
                Email = plumber.Email,
                Id = plumber.Id,
                PhoneNumber = plumber.PhoneNumber,
                Address = plumber.Address
            };
        }
    }
}