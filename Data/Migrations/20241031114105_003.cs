using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SerhendKumbara.Data.Migrations
{
    /// <inheritdoc />
    public partial class _003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionID",
                table: "Placemarks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Placemarks_RegionID",
                table: "Placemarks",
                column: "RegionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Placemarks_Regions_RegionID",
                table: "Placemarks",
                column: "RegionID",
                principalTable: "Regions",
                principalColumn: "RegionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Placemarks_Regions_RegionID",
                table: "Placemarks");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Placemarks_RegionID",
                table: "Placemarks");

            migrationBuilder.DropColumn(
                name: "RegionID",
                table: "Placemarks");
        }
    }
}
