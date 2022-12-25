using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apricot.Migrations
{
    /// <inheritdoc />
    public partial class addedisadminandbackgroundimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "SpaceUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "Spaces",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "SpaceUsers");

            migrationBuilder.DropColumn(
                name: "Background",
                table: "Spaces");
        }
    }
}
