namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P01_StudentSystem.Data.Models;
    using static DataValidations.CourseValidation;

    public class CourseConfiguration
        : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .HasKey(c => c.CourseId);

            builder
                .Property(n => n.Name)
                .HasMaxLength(MaxCourseNameLenght)
                .IsUnicode(true)
                .IsRequired(true);

            builder
                .Property(d => d.Description)
                .IsUnicode(true)
                .IsRequired(false);

            builder
                .Property(sd => sd.StartDate)
                .HasColumnType("DATETIME2")
                .IsRequired(true);

            builder
                .Property(ed => ed.EndDate)
                .HasColumnType("DATETIME2")
                .IsRequired(true);

            builder
                .Property(p => p.Price)
                .HasColumnType("DECIMAL(18,2)")
                .IsRequired(true);
        }
    }
}
