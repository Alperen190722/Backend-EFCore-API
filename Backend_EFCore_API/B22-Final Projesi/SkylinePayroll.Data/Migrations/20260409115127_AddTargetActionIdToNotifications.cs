using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkylinePayroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTargetActionIdToNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetActionId",
                table: "Notifications",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetActionId",
                table: "Notifications");
        }
    }
}
