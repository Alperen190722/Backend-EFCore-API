using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkylinePayroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTerminationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Terminations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Terminations");
        }
    }
}
