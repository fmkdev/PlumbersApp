using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PlumbingService.Models.Entities;

namespace PlumbingService.Context
{
    public class ContextApp : DbContext
    {
        public ContextApp(DbContextOptions<ContextApp> options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Plumber> Plumbers { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }
}