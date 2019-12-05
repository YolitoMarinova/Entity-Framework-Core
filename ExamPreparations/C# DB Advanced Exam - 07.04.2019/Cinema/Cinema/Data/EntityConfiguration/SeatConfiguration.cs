namespace Cinema.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Data.Models;

    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> seat)
        {
            seat
               .HasOne(s => s.Hall)
               .WithMany(h => h.Seats)
               .HasForeignKey(s => s.HallId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
