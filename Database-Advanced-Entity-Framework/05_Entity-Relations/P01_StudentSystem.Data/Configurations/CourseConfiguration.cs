using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.CourseId);

            builder.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(80);

            builder.Property(x => x.Description).IsUnicode().IsRequired(false);

            builder.Property(x => x.StartDate).IsRequired().HasColumnType("DATETIME2");

            builder.Property(x => x.EndDate).IsRequired().HasColumnType("DATETIME2");

            builder.Property(x => x.Price).IsRequired();
        }
    }
}
