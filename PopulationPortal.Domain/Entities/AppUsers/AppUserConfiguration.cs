using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PopulationPortal.Domain.Entities.Common;

namespace PopulationPortal.Domain.Entities.AppUsers
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(128);



            builder.ConfigureCommonProperties();



        }
    }
}
