using System.Collections.Generic;
using System.Linq;
using PlumbingService.Context;
using PlumbingService.DTOs;
using PlumbingService.Interfaces.IRepositories;
using PlumbingService.Models.Entities;

namespace PlumbingService.Implementation.Repositories
{
    public class PlumberRepository : IPlumberRepository
    {
         private readonly ContextApp _contextApp;

        public PlumberRepository(ContextApp contextApp)
        {
            _contextApp = contextApp;
        }
        public bool Create(Plumber plumber)
        {
            _contextApp.Plumbers.Add(plumber);
            _contextApp.SaveChanges();
            return true;
        }

        public void Delete(Plumber plumber)
        {
            _contextApp.Plumbers.Remove(plumber);
            _contextApp.SaveChanges();
        }

        public Plumber Get(int id)
        {
            return _contextApp.Plumbers.Find(id);
        }

        public IEnumerable<PlumberDTO> GetAll()
        {
            return _contextApp.Plumbers.ToList().Select(plumber => new PlumberDTO 
            {
                Id = plumber.Id,
                FirstName = plumber.FirstName,
                LastName = plumber.LastName,
                Email = plumber.Email,
                PlumberPhoto = plumber.PlumberPhoto,
                PhoneNumber = plumber.PhoneNumber
            });
        }

        public Plumber GetByMail(string email)
        {
            return _contextApp.Plumbers.SingleOrDefault(plumber => plumber.Email == email);
        }

        public bool Update(Plumber plumber)
        {
            _contextApp.Plumbers.Update(plumber);
            _contextApp.SaveChanges();
            return true;
        }
    }
}