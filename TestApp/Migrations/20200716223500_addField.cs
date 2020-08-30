using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TestApp.Migrations
{
    public partial class addField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Persons",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CountryId",
                table: "Persons",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Country_CountryId",
                table: "Persons",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Country_CountryId",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropIndex(
                name: "IX_Persons_CountryId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Persons");
        }
    }
}
