namespace P01_StudentSystem.Data.Configurations.Seeds
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class ResourceSeedsConfiguration
        : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder
                .HasData(
                new Resource
                {
                    ResourceId = 1,
                    Name = "Db Resource",
                    Url = "http\\db.com",
                    ResourceType = ResourceType.Document,
                    CourseId = 1
                },
                 new Resource
                 {
                     ResourceId = 2,
                     Name = "C# Advances Resource",
                     Url = "http\\advanced.com",
                     ResourceType = ResourceType.Presentation,
                     CourseId = 4
                 },
                  new Resource
                  {
                      ResourceId = 3,
                      Name = "C# OOP Resource",
                      Url = "http\\oop.com",
                      ResourceType = ResourceType.Document,
                      CourseId = 4
                  },
                   new Resource
                   {
                       ResourceId = 4,
                       Name = "Entity Framework",
                       Url = "http\\entityframework.com",
                       ResourceType = ResourceType.Document,
                       CourseId = 2
                   },
                    new Resource
                    {
                        ResourceId = 5,
                        Name = "Db Resource",
                        Url = "http\\db.com",
                        ResourceType = ResourceType.Document,
                        CourseId = 5
                    }
                   );
        }
    }
}
