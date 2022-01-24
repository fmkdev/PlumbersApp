using System.Collections.Generic;

namespace PlumbingService.Models.Entities
{
    public class Plumber : User
    {
        public string PlumberPhoto { get; set; }

        public string Address { get; set; }

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}