using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStudy.RazorPages.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationForMembershipandtechstudyrel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechStudyUserId",
                table: "Applications",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_TechStudyUserId",
                table: "Applications",
                column: "TechStudyUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_TechStudyUserId",
                table: "Applications",
                column: "TechStudyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_TechStudyUserId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_TechStudyUserId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TechStudyUserId",
                table: "Applications");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Applications",
                type: "longtext",
                nullable: false);
        }
    }
}
