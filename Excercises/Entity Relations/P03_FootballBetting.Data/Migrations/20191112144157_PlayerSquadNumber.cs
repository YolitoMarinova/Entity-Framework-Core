using Microsoft.EntityFrameworkCore.Migrations;

namespace P03_FootballBetting.Data.Migrations
{
    public partial class PlayerSquadNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SquadNumber",
                table: "Players",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SquadNumber",
                table: "Players",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
