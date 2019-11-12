namespace P03_FootballBetting.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_FootballBetting.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration
        : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.UserId);

            builder
                .Property(u => u.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(true);

            builder
                .Property(p => p.Password)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder
                .Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired(true);

            builder
                .Property(n => n.Name)
                .HasMaxLength(100)
                .IsUnicode(true)
                .IsRequired(true);

            builder
                .Property(b => b.Balance)
                .HasColumnType("DECIMAL(20,2)")
                .IsRequired(true);
        }
    }
}
