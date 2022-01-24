using System;
using PlumbingService.Enums;

namespace PlumbingService.Models.Entities
{
    public class Job
    {
        public int Id { get; set; }

        public string JobReference { get; set; }

        public JobType JobType { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public JobStatus JobStatus { get; set; }

        public int CustomerId { get; set; }

        public Customer customer { get; set; }

        public int? PlumberId { get; set; }

        public Plumber Plumber { get; set; }

        public string PlumberReport { get; set; }

        public string CustomerReport { get; set; }
    }
}