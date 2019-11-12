namespace P01_StudentSystem.Data.Configurations.Seeds
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P01_StudentSystem.Data.Models;

    public class StudentSeedsConfiguration
        : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .HasData(
                new Student
                {
                    StudentId = 1,
                    Name = "Gosho",
                    PhoneNumber = "0879658989",
                    Birthday = null,
                    RegisteredOn = DateTime.Now
                },
                new Student
                {
                    StudentId = 2,
                    Name = "Pesho",
                    PhoneNumber = "0879239989",
                    Birthday = new DateTime(1996, 10, 12),
                    RegisteredOn = DateTime.Now
                },
                new Student
                {
                    StudentId = 3,
                    Name = "Mariela",
                    PhoneNumber = "0896595969",
                    Birthday = new DateTime(1998, 10, 10),
                    RegisteredOn = DateTime.Now
                },
                new Student
                {
                    StudentId = 4,
                    Name = "Maggaret",
                    PhoneNumber = "0896969866",
                    Birthday = new DateTime(1978, 05, 30),
                    RegisteredOn = DateTime.Now
                },
                new Student
                {
                    StudentId = 5,
                    Name = "Michael",
                    PhoneNumber = "0877758975",
                    Birthday = null,
                    RegisteredOn = DateTime.Now
                });
        }
    }
}
