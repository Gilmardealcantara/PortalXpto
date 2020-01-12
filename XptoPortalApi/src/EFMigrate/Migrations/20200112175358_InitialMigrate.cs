using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFMigrate.Migrations
{
    public partial class InitialMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "XptoPortalApiApps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(type: "varchar(500)", nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XptoPortalApiApps", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "XptoPortalApiApps",
                columns: new[] { "Id", "Title", "Url" },
                values: new object[] { 1, "DTI", "https://dtidigital.com.br/" });

            migrationBuilder.InsertData(
                table: "XptoPortalApiApps",
                columns: new[] { "Id", "Title", "Url" },
                values: new object[] { 2, "wikipedia", "https://www.wikipedia.org/" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "XptoPortalApiApps");
        }
    }
}
