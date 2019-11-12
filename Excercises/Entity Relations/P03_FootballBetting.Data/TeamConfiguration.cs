namespace P03_FootballBetting.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P03_FootballBetting.Data.Models;

    public class TeamConfiguration
        : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder
                .HasKey(t => t.TeamId);

            builder
                .Property(n => n.Name)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder
                .Property(l => l.LogoUrl)
                .HasMaxLength(300)
                .IsUnicode(false)
                .IsRequired(false);

            builder
                .Property(i => i.Initials)
                .HasMaxLength(10)
                .IsUnicode(true)
                .IsRequired(true);

            builder
                .Property(b => b.Budget)
                .HasColumnType("DECIMAL(20,2)")
                .IsRequired(true);

            builder
                .HasOne(t => t.PrimaryKitColor)
                .WithMany(pc => pc.PrimaryKitTeams)
                .HasForeignKey(t => t.PrimaryKitColorId);

            builder
                .HasOne(t => t.SecondaryKitColor)
                .WithMany(sc => sc.SecondaryKitTeams)
                .HasForeignKey(t => t.SecondaryKitColorId);

            builder
                .HasOne(team => team.Town)
                .WithMany(town => town.Teams)
                .HasForeignKey(team => team.TownId);
        }
    }
}
