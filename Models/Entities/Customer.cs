using System.Collections.Generic;

namespace PlumbingService.Models.Entities
{
    public class Customer : User
    {
        public string CustomerPhoto { get; set; }

        public string Address { get; set; }

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}