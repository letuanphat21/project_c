using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doan.Migrations
{
    /// <inheritdoc />
    public partial class themthuoctinhchouser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmEmail",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmEmail",
                table: "users");
        }
    }
}
