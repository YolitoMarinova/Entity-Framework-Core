namespace P01_StudentSystem.Data.Configurations.Seeds
{
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    public class HomeworkSeedsConfiguration
        : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder
                .HasData(
                new Homework
                {
                    HomeworkId = 1,
                    Content = "blq blq",
                    ContentType = ContentType.Pdf,
                    SubmissionTime = DateTime.Now,
                    StudentId = 1,
                    CourseId = 2
                }, 
                new Homework
                {
                    HomeworkId = 2,
                    Content = "blq blq",
                    ContentType = ContentType.Pdf,
                    SubmissionTime = DateTime.Now,
                    StudentId = 1,
                    CourseId = 4
                },
                new Homework
                {
                    HomeworkId =3,
                    Content = "Hubava domashna",
                    ContentType = ContentType.Application,
                    SubmissionTime = DateTime.Now,
                    StudentId = 3,
                    CourseId = 5
                },
                new Homework
                {
                    HomeworkId = 4,
                    Content = "domashnata na Pesho",
                    ContentType = ContentType.Pdf,
                    SubmissionTime = DateTime.Now,
                    StudentId = 2,
                    CourseId = 4
                },
                new Homework
                {
                    HomeworkId = 5,
                    Content = "blq blq",
                    ContentType = ContentType.Pdf,
                    SubmissionTime = DateTime.Now,
                    StudentId = 5,
                    CourseId = 3
                });
        }
    }
}
