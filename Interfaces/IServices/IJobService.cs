using System.Collections.Generic;
using PlumbingService.DTOs;

namespace PlumbingService.Interfaces.IServices
{
    public interface IJobService
    {
        bool CreateJob(CreateJobRequestModel model, int id);

        JobDTO GetJob(int id);

        IEnumerable<JobDTO> ViewCustomerJobs(int id);

        IEnumerable<JobDTO> ViewPlumberJobs(int id);

        IEnumerable<JobDTO> GetInitializedJob();

        IEnumerable<JobDTO> GetAssignedJobs();

        IEnumerable<JobDTO> GetAllAcceptJobs();

        IEnumerable<JobDTO> GetAllPlumberAcceptJobs(int id);

        IEnumerable<JobDTO> GetAllCustomerDoneJobs(int id);

        IEnumerable<JobDTO> GetAllVerifiedJobs();

        IEnumerable<JobDTO> GetAllCompletedJobs();

        IEnumerable<JobDTO> GetAllCreatedJobs();

        bool UpdateJobStatusToAssigned(int id);

        bool UpdateJobStatusToAccept(int id);

        bool UpdateJobStatusToCompleted(int id);

        bool UpdatePlumberId(int id, int plumberId);

        bool SubmitCustomerReport(CustomerReportModel model, int id);

        bool SubmitPlumberReport(PlumberReportModel model, int id);




    }
}