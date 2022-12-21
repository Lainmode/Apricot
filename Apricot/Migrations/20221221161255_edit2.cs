using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apricot.Migrations
{
    /// <inheritdoc />
    public partial class edit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_Users_UserID",
                table: "Spaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_UserID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Spaces_UserID",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Spaces");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Users",
                newName: "SpaceID");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserID",
                table: "Users",
                newName: "IX_Users_SpaceID");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID1 = table.Column<int>(type: "int", nullable: false),
                    UserID2 = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contacts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserID",
                table: "Contacts",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Spaces_SpaceID",
                table: "Users",
                column: "SpaceID",
                principalTable: "Spaces",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Spaces_SpaceID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.RenameColumn(
                name: "SpaceID",
                table: "Users",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Users_SpaceID",
                table: "Users",
                newName: "IX_Users_UserID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_UserID",
                table: "Users",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");
        }
    }
}
