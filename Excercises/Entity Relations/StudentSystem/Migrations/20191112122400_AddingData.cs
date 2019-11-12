using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P01_StudentSystem.Migrations
{
    public partial class AddingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "Description", "EndDate", "Name", "Price", "StartDate" },
                values: new object[,]
                {
                    { 1, "Best Database Course", new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Database Basic", 260m, new DateTime(2019, 11, 12, 14, 23, 59, 584, DateTimeKind.Local).AddTicks(721) },
                    { 2, null, new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Entity Framework Core", 360m, new DateTime(2019, 11, 12, 14, 23, 59, 587, DateTimeKind.Local).AddTicks(988) },
                    { 3, "Курс за начинаещи", new DateTime(2019, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Programing Basics", 440m, new DateTime(2019, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, null, new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# Advances", 260m, new DateTime(2019, 11, 12, 14, 23, 59, 587, DateTimeKind.Local).AddTicks(1025) },
                    { 5, null, new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# OOP", 260m, new DateTime(2019, 11, 12, 14, 23, 59, 587, DateTimeKind.Local).AddTicks(1031) }
                });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1,
                column: "RegisteredOn",
                value: new DateTime(2019, 11, 12, 14, 23, 59, 593, DateTimeKind.Local).AddTicks(1465));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2,
                column: "RegisteredOn",
                value: new DateTime(2019, 11, 12, 14, 23, 59, 593, DateTimeKind.Local).AddTicks(2307));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 3,
                column: "RegisteredOn",
                value: new DateTime(2019, 11, 12, 14, 23, 59, 593, DateTimeKind.Local).AddTicks(2325));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 4,
                column: "RegisteredOn",
                value: new DateTime(2019, 11, 12, 14, 23, 59, 593, DateTimeKind.Local).AddTicks(2330));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 5,
                column: "RegisteredOn",
                value: new DateTime(2019, 11, 12, 14, 23, 59, 593, DateTimeKind.Local).AddTicks(2335));

            migrationBuilder.InsertData(
                table: "HomeworkSubmissions",
                columns: new[] { "HomeworkId", "Content", "ContentType", "CourseId", "StudentId", "SubmissionTime" },
                values: new object[,]
                {
                    { 1, "blq blq", 2, 2, 1, new DateTime(2019, 11, 12, 14, 23, 59, 590, DateTimeKind.Local).AddTicks(1223) },
                    { 5, "blq blq", 2, 3, 5, new DateTime(2019, 11, 12, 14, 23, 59, 590, DateTimeKind.Local).AddTicks(3296) },
                    { 2, "blq blq", 2, 4, 1, new DateTime(2019, 11, 12, 14, 23, 59, 590, DateTimeKind.Local).AddTicks(3261) },
                    { 4, "domashnata na Pesho", 2, 4, 2, new DateTime(2019, 11, 12, 14, 23, 59, 590, DateTimeKind.Local).AddTicks(3292) },
                    { 3, "Hubava domashna", 1, 5, 3, new DateTime(2019, 11, 12, 14, 23, 59, 590, DateTimeKind.Local).AddTicks(3287) }
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "ResourceId", "CourseId", "Name", "ResourceType", "Url" },
                values: new object[,]
                {
                    { 1, 1, "Db Resource", 3, "http\\db.com" },
                    { 4, 2, "Entity Framework", 3, "http\\entityframework.com" },
                    { 2, 4, "C# Advances Resource", 2, "http\\advanced.com" },
                    { 3, 4, "C# OOP Resource", 3, "http\\oop.com" },
                    { 5, 5, "Db Resource", 3, "http\\db.com" }
                });

            migrationBuilder.InsertData(
                table: "StudentCourses",
                columns: new[] { "StudentId", "CourseId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 1, 2 },
                    { 5, 3 },
                    { 4, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HomeworkSubmissions",
                keyColumn: "HomeworkId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HomeworkSubmissions",
                keyColumn: "HomeworkId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HomeworkSubmissions",
                keyColumn: "HomeworkId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HomeworkSubmissions",
                keyColumn: "HomeworkId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "HomeworkSubmissions",
                keyColumn: "HomeworkId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "ResourceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "ResourceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "ResourceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "ResourceId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "ResourceId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "StudentCourses",
                keyColumns: new[] { "StudentId", "CourseId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "StudentCourses",
                keyColumns: new[] { "StudentId", "CourseId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "StudentCourses",
                keyColumns: new[] { "StudentId", "CourseId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "StudentCourses",
                keyColumns: new[] { "StudentId", "CourseId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "StudentCourses",
                keyColumns: new[] { "StudentId", "CourseId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1,
                column: "RegisteredOn",
                value: new DateTime(2019, 11, 12, 13, 59, 9, 933, DateTimeKind.Local).AddTicks(5230));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2,
                column: "RegisteredOn",
                value: new DateTime(2019, 11, 12, 13, 59, 9, 936, DateTimeKind.Local).AddTicks(4047));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 3,
                column: "RegisteredOn",
                value: new DateTime(2019, 11, 12, 13, 59, 9, 936, DateTimeKind.Local).AddTicks(4066));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 4,
                column: "RegisteredOn",
                value: new DateTime(2019, 11, 12, 13, 59, 9, 936, DateTimeKind.Local).AddTicks(4072));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 5,
                column: "RegisteredOn",
                value: new DateTime(2019, 11, 12, 13, 59, 9, 936, DateTimeKind.Local).AddTicks(4077));
        }
    }
}
