using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.FirstName).IsRequired().IsUnicode().HasMaxLength(50);

            builder.Property(x => x.LastName).IsRequired().IsUnicode().HasMaxLength(50);

            builder.Property(x => x.Email).IsRequired().IsUnicode(false).HasMaxLength(80);

            builder.Property(x => x.Password).IsRequired().IsUnicode(false).HasMaxLength(25);
        }
    }
}
