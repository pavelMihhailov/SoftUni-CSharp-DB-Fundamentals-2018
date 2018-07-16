using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(x => x.PlayerId);

            builder.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(50);

            builder.Property(x => x.IsInjured).HasDefaultValue(false);

            builder.HasOne(x => x.Team).WithMany(x => x.Players).HasForeignKey(x => x.TeamId);

            builder.HasOne(x => x.Position).WithMany(x => x.Players).HasForeignKey(x => x.PositionId);
        }
    }
}
