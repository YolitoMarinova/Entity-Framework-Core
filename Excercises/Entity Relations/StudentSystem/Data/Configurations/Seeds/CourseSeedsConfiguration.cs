namespace P01_StudentSystem.Data.Configurations.Seeds
{
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    public class CourseSeedsConfiguration
        : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .HasData(
                new Course
                {
                    CourseId = 1,
                    Name = "Database Basic",
                    Description = "Best Database Course",
                    StartDate = DateTime.Now,
                    EndDate = new DateTime(2020, 03, 01),
                    Price = 260m
                },
                new Course
                {
                    CourseId = 2,
                    Name = "Entity Framework Core",
                    Description = null,
                    StartDate = DateTime.Now,
                    EndDate = new DateTime(2020, 04, 01),
                    Price = 360m
                },
                new Course
                {
                    CourseId = 3,
                    Name = "Programing Basics",
                    Description = "Курс за начинаещи",
                    StartDate = new DateTime(2019,07,26),
                    EndDate = new DateTime(2019, 10, 31),
                    Price = 440m
                },
                new Course
                {
                    CourseId = 4,
                    Name = "C# Advances",
                    Description = null,
                    StartDate = DateTime.Now,
                    EndDate = new DateTime(2020, 03, 01),
                    Price = 260m
                },
                new Course
                {
                    CourseId = 5,
                    Name = "C# OOP",
                    Description = null,
                    StartDate = DateTime.Now,
                    EndDate = new DateTime(2020, 03, 01),
                    Price = 260m
                });
        }
    }
}
