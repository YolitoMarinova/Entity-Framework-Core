namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class HomeworkConfiguration
        : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder
                .HasKey(h => h.HomeworkId);

            builder
                .Property(c => c.Content)
                .IsUnicode(false)
                .IsRequired(true);

            builder
                .Property(ct => ct.ContentType)
                .IsRequired(true);

            builder
                .Property(s => s.SubmissionTime)
                .HasColumnType("DATETIME2")
                .IsRequired(true);

            builder
                .HasOne(h => h.Student)
                .WithMany(s => s.HomeworkSubmissions)
                .HasForeignKey(h => h.StudentId);

            builder
                .HasOne(h => h.Course)
                .WithMany(c => c.HomeworkSubmissions)
                .HasForeignKey(h => h.CourseId);
        }
    }
}
