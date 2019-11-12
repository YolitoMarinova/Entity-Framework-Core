namespace P03_FootballBetting.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_FootballBetting.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PlayerConfiguration
        : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder
                .HasKey(p => p.PlayerId);

            builder
                .Property(n => n.Name)
                .HasMaxLength(90)
                .IsUnicode(true)
                .IsRequired(true);

            builder
                .Property(s => s.SquadNumber)
                .IsRequired(true);

            builder
                .Property(i => i.IsInjured)
                .IsRequired(true);

            builder
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);

            builder
                .HasOne(p => p.Position)
                .WithMany(po => po.Players)
                .HasForeignKey(p => p.PositionId);
        }
    }
}
