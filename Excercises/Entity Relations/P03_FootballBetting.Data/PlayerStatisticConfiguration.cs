namespace P03_FootballBetting.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_FootballBetting.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PlayerStatisticConfiguration
        : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
        {
            builder
                .HasKey(pg => new { pg.PlayerId, pg.GameId });

            builder
                .HasOne(pg => pg.Game)
                .WithMany(g => g.PlayerStatistics)
                .HasForeignKey(pg => pg.GameId);

            builder
                .HasOne(pg => pg.Player)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(pg => pg.PlayerId);
        }
    }
}
