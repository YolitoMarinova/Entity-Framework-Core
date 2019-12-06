namespace MusicHub.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class SongPerformerConfiguration : IEntityTypeConfiguration<SongPerformer>
    {
        public void Configure(EntityTypeBuilder<SongPerformer> songPerformer)
        {
            songPerformer
                .HasKey(sp => new { sp.SongId, sp.PerformerId });

            songPerformer
                .HasOne(sp => sp.Song)
                .WithMany(s => s.SongPerformers)
                .HasForeignKey(sp => sp.SongId)
                .OnDelete(DeleteBehavior.Cascade);

            songPerformer
                .HasOne(sp => sp.Performer)
                .WithMany(p => p.PerformerSongs)
                .HasForeignKey(sp => sp.PerformerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
