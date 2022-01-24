using System.Collections.Generic;
using PlumbingService.DTOs;
using PlumbingService.Models.Entities;

namespace PlumbingService.Interfaces.IRepositories
{
    public interface IJobRepository
    {
        bool CreateJob(Job job);

        IEnumerable<JobDTO> ViewCustomerJobs(int id);

        IEnumerable<JobDTO> ViewPlumberJobs(int id);

        IEnumerable<JobDTO> GetInitializedJob();

        IEnumerable<JobDTO> GetAssignedJobs();

        IEnumerable<JobDTO> GetAllCreatedJobs();

        IEnumerable<JobDTO> GetAllAcceptJobs();

        IEnumerable<JobDTO> GetAllPlumberAcceptJobs(int id);

        IEnumerable<JobDTO> GetAllCustomerDoneJobs(int id);

        IEnumerable<JobDTO> GetAllVerifiedJobs();

        IEnumerable<JobDTO> GetAllCompletedJobs();

        bool Update(Job job);

        Job GetJob(int id);
    }
}