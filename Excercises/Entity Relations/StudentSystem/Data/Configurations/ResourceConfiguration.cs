namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P01_StudentSystem.Data.Models;
    using static DataValidations.ResourceValidation;

    public class ResourceConfiguration
        : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder
                .HasKey(r => r.ResourceId);

            builder
                .Property(n => n.Name)
                .HasMaxLength(MaxNameLenght)
                .IsUnicode(true)
                .IsRequired(true);

            builder
                .Property(u => u.Url)
                .IsUnicode(false)
                .IsRequired(true);

            builder
                .Property(rt => rt.ResourceType)
                .IsRequired(true);

            builder
                .HasOne(r => r.Course)
                .WithMany(c => c.Resources)
                .HasForeignKey(r => r.CourseId);
        }
    }
}
