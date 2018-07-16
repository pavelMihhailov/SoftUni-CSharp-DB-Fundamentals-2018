using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasKey(x => x.TownId);

            builder.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(100);

            builder.HasOne(x => x.Country).WithMany(x => x.Towns).HasForeignKey(x => x.CountryId);
        }
    }
}
