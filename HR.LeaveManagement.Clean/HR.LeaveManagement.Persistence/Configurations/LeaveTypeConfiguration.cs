using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Persistence.Configurations
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(70);

            builder.Property(e => e.DefaultDays)
                .IsRequired();


            builder.HasData(new LeaveType
            {
                Id = 1,
                Name = "Vacation",
                DefaultDays = 10,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            });
        }
    }
}
