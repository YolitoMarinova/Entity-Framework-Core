namespace P01_StudentSystem.Data.Configurations.Seeds
{
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class StudentCourseSeedsConfiguration
        : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder
                .HasData(
                new StudentCourse
                {
                    StudentId = 1,
                    CourseId = 1
                },
                new StudentCourse
                {
                    StudentId = 2,
                    CourseId = 1
                },
                new StudentCourse
                {
                    StudentId = 1,
                    CourseId = 2
                },
                new StudentCourse
                {
                    StudentId = 5,
                    CourseId = 3
                },
                new StudentCourse
                {
                    StudentId = 4,
                    CourseId = 3
                });
        }
    }
}
