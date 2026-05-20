using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkylinePayroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHierarchyLevelToRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HierarchyLevel",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HierarchyLevel",
                table: "Roles");
        }
    }
}
