using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apricot.Migrations
{
    /// <inheritdoc />
    public partial class edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Spaces",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_UserID",
                table: "Spaces",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_Users_UserID",
                table: "Spaces",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_Users_UserID",
                table: "Spaces");

            migrationBuilder.DropIndex(
                name: "IX_Spaces_UserID",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Spaces");
        }
    }
}
