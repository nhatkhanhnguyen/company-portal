using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateArticleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageResourceId",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "CoverImageId",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CoverImageId",
                table: "Articles",
                column: "CoverImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Resources_CoverImageId",
                table: "Articles",
                column: "CoverImageId",
                principalTable: "Resources",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Resources_CoverImageId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_CoverImageId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "CoverImageId",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "CoverImageResourceId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
