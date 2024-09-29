using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStudy.RazorPages.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationForMembershipandgrouprel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Applications_GroupId",
                table: "Applications",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Groups_GroupId",
                table: "Applications",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Groups_GroupId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_GroupId",
                table: "Applications");
        }
    }
}
