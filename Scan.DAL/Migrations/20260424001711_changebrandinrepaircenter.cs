using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scan.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changebrandinrepaircenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeverityLevel",
                table: "RepairCenters");

            migrationBuilder.AddColumn<string>(
                name: "SupportedBrand",
                table: "RepairCenters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupportedBrand",
                table: "RepairCenters");

            migrationBuilder.AddColumn<string>(
                name: "SeverityLevel",
                table: "RepairCenters",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
