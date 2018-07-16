using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.Configurations
{
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.HasKey(x => x.HomeworkId);

            builder.Property(x => x.Content).IsRequired().IsUnicode(false);

            builder.Property(x => x.ContentType).IsRequired();

            builder.Property(x => x.SubmissionTime).IsRequired().HasColumnType("DATETIME2");

            builder.HasOne(x => x.Student).WithMany(x => x.HomeworkSubmissions).HasForeignKey(x => x.StudentId);

            builder.HasOne(x => x.Course).WithMany(x => x.HomeworkSubmissions).HasForeignKey(x => x.CourseId);
        }
    }
}
