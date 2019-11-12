using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P01_StudentSystem.Migrations
{
    public partial class SeedStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Birthday", "Name", "PhoneNumber", "RegisteredOn" },
                values: new object[,]
                {
                    { 1, null, "Gosho", "0879658989", new DateTime(2019, 11, 12, 13, 59, 9, 933, DateTimeKind.Local).AddTicks(5230) },
                    { 2, new DateTime(1996, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pesho", "0879239989", new DateTime(2019, 11, 12, 13, 59, 9, 936, DateTimeKind.Local).AddTicks(4047) },
                    { 3, new DateTime(1998, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mariela", "0896595969", new DateTime(2019, 11, 12, 13, 59, 9, 936, DateTimeKind.Local).AddTicks(4066) },
                    { 4, new DateTime(1978, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maggaret", "0896969866", new DateTime(2019, 11, 12, 13, 59, 9, 936, DateTimeKind.Local).AddTicks(4072) },
                    { 5, null, "Michael", "0877758975", new DateTime(2019, 11, 12, 13, 59, 9, 936, DateTimeKind.Local).AddTicks(4077) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 5);
        }
    }
}
