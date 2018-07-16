using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(x => x.TeamId);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);

            builder.Property(x => x.LogoUrl).IsUnicode(false);

            builder.Property(x => x.Initials).IsRequired().HasColumnType("NCHAR(3)");

            builder.HasOne(x => x.PrimaryKitColor).WithMany(pc => pc.PrimaryKitTeams)
                .HasForeignKey(x => x.PrimaryKitColorId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.SecondaryKitColor).WithMany(pc => pc.SecondaryKitTeams)
                .HasForeignKey(x => x.SecondaryKitColorId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Town).WithMany(t => t.Teams).HasForeignKey(x => x.TownId);
        }
    }
}
