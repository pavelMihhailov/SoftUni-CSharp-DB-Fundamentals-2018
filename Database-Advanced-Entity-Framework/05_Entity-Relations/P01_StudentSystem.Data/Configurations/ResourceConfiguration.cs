﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.Configurations
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasKey(x => x.CourseId);

            builder.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(50);

            builder.Property(x => x.Url).IsRequired().IsUnicode(false);

            builder.Property(x => x.ResourceType).IsRequired();

            builder.HasOne(x => x.Course).WithMany(x => x.Resources).HasForeignKey(x => x.CourseId);
        }
    }
}
