using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIndexesAndCategoryImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_BlobName",
                table: "Resources",
                column: "BlobName");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_Name",
                table: "Resources",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FullDescription",
                table: "Products",
                column: "FullDescription");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Description",
                table: "Categories",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ExternalId",
                table: "Categories",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ImageId",
                table: "Categories",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Description",
                table: "Articles",
                column: "Description");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Resources_ImageId",
                table: "Categories",
                column: "ImageId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Resources_ImageId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Resources_BlobName",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_Name",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Products_FullDescription",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Description",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ExternalId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ImageId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Articles_Description",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Categories");
        }
    }
}
