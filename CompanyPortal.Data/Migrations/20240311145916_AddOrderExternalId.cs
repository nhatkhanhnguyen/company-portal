using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderExternalId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Email",
                table: "Orders",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ExternalId",
                table: "Orders",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Fullname",
                table: "Orders",
                column: "Fullname");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PhoneNumber",
                table: "Orders",
                column: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_Email",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ExternalId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Fullname",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PhoneNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Orders");
        }
    }
}
