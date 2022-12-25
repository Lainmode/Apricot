using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apricot.Migrations
{
    /// <inheritdoc />
    public partial class addedpicturetospace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Spaces",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Spaces");
        }
    }
}
