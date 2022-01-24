using System.Collections.Generic;
using PlumbingService.DTOs;

namespace PlumbingService.Interfaces.IServices
{
    public interface IPlumberService
    {
        bool Create(CreatePlumberRequestModel model);

        bool Update(UpdatePlumberRequestModel model, int id);

        void Delete(int id);

        PlumberDTO Get(int id);

        IEnumerable<PlumberDTO> GetAll();

        PlumberDTO GetByMail(string email);

        PlumberDTO Login(LoginPlumberRequestModel model);
    }
}