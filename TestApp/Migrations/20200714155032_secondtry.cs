using Microsoft.EntityFrameworkCore.Migrations;

namespace TestApp.Migrations
{
    public partial class secondtry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Genders",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "Genders",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
