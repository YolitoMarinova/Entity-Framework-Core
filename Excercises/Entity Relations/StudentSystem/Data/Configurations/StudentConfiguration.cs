namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static DataValidations.StudentValidation;
    public class StudentConfiguration
        : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .HasKey(s => s.StudentId);

            builder
                .Property(n => n.Name)
                .HasMaxLength(MaxNameLenght)
                .IsUnicode(true)
                .IsRequired(true);

            builder
                .Property(pn => pn.PhoneNumber)
                .IsUnicode(false)
                .IsRequired(false);

            builder
                .Property(r => r.RegisteredOn)
                .HasColumnType("DATETIME2")
                .IsRequired(true);

            builder
                .Property(b => b.Birthday)
                .IsRequired(false);
        }
    }
}
