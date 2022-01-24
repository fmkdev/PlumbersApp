using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PlumbingService.Context;
using PlumbingService.DTOs;
using PlumbingService.Enums;
using PlumbingService.Interfaces.IRepositories;
using PlumbingService.Models.Entities;

namespace PlumbingService.Implementation.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly ContextApp _contextApp;

        public JobRepository(ContextApp contextApp)
        {
            _contextApp = contextApp;
        }
        public bool CreateJob(Job job)
        {
            _contextApp.Jobs.Add(job);
            _contextApp.SaveChanges();
            return true;
        }

        public IEnumerable<JobDTO> GetAssignedJobs()
        {
            return _contextApp.Jobs
            .Include(c => c.customer)
            .Include(p => p.Plumber)
            .Where(job => job.JobStatus == JobStatus.Assigned)
            .Select(job => new JobDTO
            {
                Id = job.Id,
                CustomerName = $"{job.customer.FirstName} {job.customer.LastName}",
                CustomerNumber = job.customer.PhoneNumber,
                PlumberName = $"{job.Plumber.FirstName}{job.Plumber.LastName}",
                PlumberNumber = job.Plumber.PhoneNumber,
                CreationTime = job.CreationTime,
                Description = job.Description,
                JobReference = job.JobReference,
                JobStatus = job.JobStatus,
                JobType = job.JobType
            }).ToList();
        }

        public IEnumerable<JobDTO> GetAllCreatedJobs()
        {
            return _contextApp.Jobs
           .Include(c => c.customer)
           .Include(p => p.Plumber)
           .ToList()
           .Select(job => new JobDTO
           {
               Id = job.Id,
               CustomerName = $"{job.customer.FirstName} {job.customer.LastName}",
               CustomerNumber = job.customer.PhoneNumber,
               PlumberName = $"{job.Plumber.FirstName}{job.Plumber.LastName}",
               PlumberNumber = job.Plumber.PhoneNumber,
               CreationTime = job.CreationTime,
               Description = job.Description,
               JobReference = job.JobReference,
               JobStatus = job.JobStatus,
               JobType = job.JobType
           });
        }

        public IEnumerable<JobDTO> GetAssignedJob()
        {

            return _contextApp.Jobs.Include(c => c.customer).Where(job => job.JobStatus == JobStatus.Assigned).Select(job => new JobDTO
            {
                Id = job.Id,
                CustomerName = $"{job.customer.FirstName} {job.customer.LastName}",
                CustomerNumber = job.customer.PhoneNumber,
                CreationTime = job.CreationTime,
                Description = job.Description,
                JobReference = job.JobReference,
                JobStatus = job.JobStatus,
                JobType = job.JobType
            }).ToList();
        }

        public IEnumerable<JobDTO> GetInitializedJob()
        {
            return _contextApp.Jobs.Include(c => c.customer)
            .Where(job => job.JobStatus == JobStatus.Initialized)
            .Select(job => new JobDTO
            {
                Id = job.Id,
                CustomerName = $"{job.customer.FirstName} {job.customer.LastName}",
                CustomerNumber = job.customer.PhoneNumber,
                CreationTime = job.CreationTime,
                Description = job.Description,
                JobReference = job.JobReference,
                JobStatus = job.JobStatus,
                JobType = job.JobType
            }).ToList();
        }

        public IEnumerable<JobDTO> ViewCustomerJobs(int id)
        {
            return _contextApp.Jobs.Where(c => c.CustomerId == id).Select(job => new JobDTO
            {
                CreationTime = job.CreationTime,
                Description = job.Description,
                JobReference = job.JobReference,
                JobStatus = job.JobStatus,
                JobType = job.JobType
            }).ToList();
        }

        public Job GetJob(int id)
        {
            return _contextApp.Jobs.Find(id);
        }

        public bool Update(Job job)
        {
            _contextApp.Jobs.Update(job);
            _contextApp.SaveChanges();
            return true;
        }

        public IEnumerable<JobDTO> ViewPlumberJobs(int id)
        {
            return _contextApp.Jobs
            .Include(c => c.customer)
            .Include(p => p.Plumber)
            .Where(job => job.PlumberId == id)
            .Select(job => new JobDTO
            {
                Id = job.Id,
                CustomerName = $"{job.customer.FirstName} {job.customer.LastName}",
                CustomerNumber = job.customer.PhoneNumber,
                PlumberName = $"{job.Plumber.FirstName}{job.Plumber.LastName}",
                PlumberNumber = job.Plumber.PhoneNumber,
                CreationTime = job.CreationTime,
                Description = job.Description,
                JobReference = job.JobReference,
                JobStatus = job.JobStatus,
                JobType = job.JobType
            }).ToList();
        }

        public IEnumerable<JobDTO> GetAllAcceptJobs()
        {
            return _contextApp.Jobs
            .Include(c => c.customer)
            .Include(p => p.Plumber)
            .Where(job => job.JobStatus == JobStatus.Accept)
            .Select(job => new JobDTO
            {
                Id = job.Id,
                CustomerName = $"{job.customer.FirstName} {job.customer.LastName}",
                CustomerNumber = job.customer.PhoneNumber,
                PlumberName = $"{job.Plumber.FirstName}{job.Plumber.LastName}",
                PlumberNumber = job.Plumber.PhoneNumber,
                CreationTime = job.CreationTime,
                Description = job.Description,
                JobReference = job.JobReference,
                JobStatus = job.JobStatus,
                JobType = job.JobType
            }).ToList();
        }

        public IEnumerable<JobDTO> GetAllVerifiedJobs()
        {
            return _contextApp.Jobs
           .Include(c => c.customer)
           .Include(p => p.Plumber)
           .Where(job => job.JobStatus == JobStatus.Verified)
           .Select(job => new JobDTO
           {
               Id = job.Id,
               CustomerName = $"{job.customer.FirstName} {job.customer.LastName}",
               CustomerNumber = job.customer.PhoneNumber,
               PlumberName = $"{job.Plumber.FirstName}{job.Plumber.LastName}",
               PlumberNumber = job.Plumber.PhoneNumber,
               CreationTime = job.CreationTime,
               Description = job.Description,
               JobReference = job.JobReference,
               JobStatus = job.JobStatus,
               JobType = job.JobType
           }).ToList();
        }

        public IEnumerable<JobDTO> GetAllCompletedJobs()
        {
            return _contextApp.Jobs
           .Include(c => c.customer)
           .Include(p => p.Plumber)
           .Where(job => job.JobStatus == JobStatus.Completed)
           .Select(job => new JobDTO
           {
               Id = job.Id,
               CustomerName = $"{job.customer.FirstName} {job.customer.LastName}",
               CustomerNumber = job.customer.PhoneNumber,
               PlumberName = $"{job.Plumber.FirstName}{job.Plumber.LastName}",
               PlumberNumber = job.Plumber.PhoneNumber,
               CreationTime = job.CreationTime,
               Description = job.Description,
               JobReference = job.JobReference,
               JobStatus = job.JobStatus,
               JobType = job.JobType
           }).ToList();
        }

        public IEnumerable<JobDTO> GetAllPlumberAcceptJobs(int id)
        {
            return _contextApp.Jobs
           .Include(c => c.customer)
           .Include(p => p.Plumber)
           .Where(job =>job.PlumberId == id && job.JobStatus == JobStatus.Accept)
           .Select(job => new JobDTO
           {
               Id = job.Id,
               CustomerName = $"{job.customer.FirstName} {job.customer.LastName}",
               CustomerNumber = job.customer.PhoneNumber,
               PlumberName = $"{job.Plumber.FirstName}{job.Plumber.LastName}",
               PlumberNumber = job.Plumber.PhoneNumber,
               CreationTime = job.CreationTime,
               Description = job.Description,
               JobReference = job.JobReference,
               JobStatus = job.JobStatus,
               JobType = job.JobType
           }).ToList();
        }

        public IEnumerable<JobDTO> GetAllCustomerDoneJobs(int id)
        {
                        return _contextApp.Jobs
           .Include(c => c.customer)
           .Include(p => p.Plumber)
           .Where(job =>job.CustomerId == id && job.JobStatus == JobStatus.TaskDone)
           .Select(job => new JobDTO
           {
               Id = job.Id,
               CustomerName = $"{job.customer.FirstName} {job.customer.LastName}",
               CustomerNumber = job.customer.PhoneNumber,
               PlumberName = $"{job.Plumber.FirstName}{job.Plumber.LastName}",
               PlumberNumber = job.Plumber.PhoneNumber,
               CreationTime = job.CreationTime,
               Description = job.Description,
               JobReference = job.JobReference,
               JobStatus = job.JobStatus,
               JobType = job.JobType
           }).ToList();
        }
    }
}