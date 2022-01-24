using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlumbingService.Models.Entities;

namespace PlumbingService.Context.EfConfiguration
{
    public class PlumberEntityTypeConfiguration : IEntityTypeConfiguration<Plumber>
    {
        public void Configure(EntityTypeBuilder<Plumber> builder)
        {
            
        }
    }
}