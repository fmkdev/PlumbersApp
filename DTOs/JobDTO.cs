using System;
using System.ComponentModel.DataAnnotations;
using PlumbingService.Enums;
using PlumbingService.Models.Entities;

namespace PlumbingService.DTOs
{
    public class JobDTO
    {
        public int Id { get; set; }

        public string JobReference { get; set; }

        public JobType JobType { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public JobStatus JobStatus { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerNumber { get; set; }

        public string CustomerReport { get; set; }

        public Customer customer { get; set; }

        public int? PlumberId { get; set; }

        public string PlumberName { get; set; }

        public string PlumberNumber { get; set; }

        public Plumber Plumber { get; set; }

        public string PlumberReport { get; set; }
    }
    public class CreateJobRequestModel
    {
        [Required(ErrorMessage = "Select Job Type")]
        public JobType JobType { get; set; }
        [Required(ErrorMessage = "Write Description of the Job")]
        public string Description { get; set; }
    }
    public class CustomerReportModel
    {
        [Required(ErrorMessage = "Make report, its important for our business growth")]
        [StringLength(maximumLength: 100)]
        public string CReport { get; set; }
    }
    public class PlumberReportModel
    {
        [Required(ErrorMessage = "Make report, its important for our business growth")]
        [StringLength(maximumLength: 100)]
        public string PReport { get; set; }
    }
}