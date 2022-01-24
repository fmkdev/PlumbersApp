using System;
using System.Collections.Generic;
using PlumbingService.DTOs;
using PlumbingService.Interfaces.IRepositories;
using PlumbingService.Interfaces.IServices;
using PlumbingService.Models.Entities;

namespace PlumbingService.Implementation.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public bool CreateJob(CreateJobRequestModel model, int id)
        {
            var job = new Job
            {
                CustomerId = id,
                CreationTime = DateTime.Now,
                JobType = model.JobType,
                Description = model.Description,
                JobReference = Guid.NewGuid().ToString().Substring(1, 10).Replace("-", "").Trim(),
                JobStatus = Enums.JobStatus.Initialized
            };
            return _jobRepository.CreateJob(job);
        }

        public IEnumerable<JobDTO> GetAllCreatedJobs()
        {
            return _jobRepository.GetAllCreatedJobs();
        }

        public IEnumerable<JobDTO> GetAssignedJobs()
        {
            return _jobRepository.GetAssignedJobs();
        }

        public IEnumerable<JobDTO> GetInitializedJob()
        {
            return _jobRepository.GetInitializedJob();
        }

        public IEnumerable<JobDTO> ViewCustomerJobs(int id)
        {
            return _jobRepository.ViewCustomerJobs(id);
        }

        public bool UpdateJobStatusToAccept(int id)
        {
            var job = _jobRepository.GetJob(id);
            job.JobStatus = Enums.JobStatus.Accept;
            _jobRepository.Update(job);
            return true;
        }

        public bool UpdateJobStatusToAssigned(int id)
        {
            var job = _jobRepository.GetJob(id);
            job.JobStatus = Enums.JobStatus.Assigned;
            _jobRepository.Update(job);
            return true;
        }

        public bool UpdatePlumberId(int id, int plumberId)
        {
            var jobs = _jobRepository.GetJob(id);
            jobs.PlumberId = plumberId;
            return _jobRepository.Update(jobs);
        }

        public IEnumerable<JobDTO> ViewPlumberJobs(int id)
        {
            return _jobRepository.ViewPlumberJobs(id);
        }

        public IEnumerable<JobDTO> GetAllAcceptJobs()
        {
            return _jobRepository.GetAllAcceptJobs();
        }

        public IEnumerable<JobDTO> GetAllVerifiedJobs()
        {
            return _jobRepository.GetAllVerifiedJobs();
        }

        public IEnumerable<JobDTO> GetAllCompletedJobs()
        {
            return _jobRepository.GetAllCompletedJobs();
        }

        public bool UpdateJobStatusToCompleted(int id)
        {
            var job = _jobRepository.GetJob(id);
            job.JobStatus = Enums.JobStatus.Completed;
            return _jobRepository.Update(job);
        }

        public IEnumerable<JobDTO> GetAllPlumberAcceptJobs(int id)
        {
            return _jobRepository.GetAllAcceptJobs();
        }

        public IEnumerable<JobDTO> GetAllCustomerDoneJobs(int id)
        {
            return _jobRepository.GetAllCustomerDoneJobs(id);
        }

        public bool SubmitCustomerReport(CustomerReportModel report, int id)
        {
            var job = _jobRepository.GetJob(id);
            job.JobStatus = Enums.JobStatus.Verified;
            job.CustomerReport = report.CReport;
            return _jobRepository.Update(job);
        }

        public bool SubmitPlumberReport(PlumberReportModel report, int id)
        {
            var job = _jobRepository.GetJob(id);
            job.JobStatus = Enums.JobStatus.TaskDone;
            job.PlumberReport = report.PReport;
            return _jobRepository.Update(job);
        }

        public JobDTO GetJob(int id)
        {
            var job = _jobRepository.GetJob(id);
            var Job = new JobDTO
            {
                Id = job.Id,
                CreationTime = job.CreationTime,
                Description = job.Description,
                JobReference = job.JobReference,
                JobStatus = job.JobStatus,
                JobType = job.JobType
            };
            return Job;
        }
    }
}