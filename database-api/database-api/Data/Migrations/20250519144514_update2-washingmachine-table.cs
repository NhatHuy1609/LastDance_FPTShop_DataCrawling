using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace database_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class update2washingmachinetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "WashingMachines");

            migrationBuilder.DropColumn(
                name: "isAvailable",
                table: "WashingMachines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "WashingMachines",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "isAvailable",
                table: "WashingMachines",
                type: "text",
                nullable: true);
        }
    }
}
