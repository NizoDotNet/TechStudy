using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStudy.RazorPages.Migrations
{
    /// <inheritdoc />
    public partial class nameofgroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Groups",
                type: "longtext",
                nullable: false);

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Heçbir qrupda iştirak etmirsiz.", "No Group" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Groups");

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "No Group");
        }
    }
}
