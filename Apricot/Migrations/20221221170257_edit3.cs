using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apricot.Migrations
{
    /// <inheritdoc />
    public partial class edit3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserID",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_UserID",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "UserID1",
                table: "Contacts");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserID2",
                table: "Contacts",
                column: "UserID2");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserID2",
                table: "Contacts",
                column: "UserID2",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserID2",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_UserID2",
                table: "Contacts");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Contacts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserID1",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserID",
                table: "Contacts",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserID",
                table: "Contacts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");
        }
    }
}
