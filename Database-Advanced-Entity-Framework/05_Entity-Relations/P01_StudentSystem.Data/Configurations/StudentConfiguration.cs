using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.StudentId);

            builder.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(100);

            builder.Property(x => x.PhoneNumber).IsRequired(false).IsUnicode(false).HasColumnType("CHAR(10)");

            builder.Property(x => x.RegisteredOn).IsRequired().HasColumnType("DATETIME2");

            builder.Property(x => x.Birthday).IsRequired(false).HasColumnType("DATETIME2");
        }
    }
}
