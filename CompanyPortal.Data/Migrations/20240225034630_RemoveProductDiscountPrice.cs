using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductDiscountPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountedPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PercentDiscount",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountedPrice",
                table: "Products",
                type: "decimal(11,0)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentDiscount",
                table: "Products",
                type: "decimal(11,0)",
                nullable: true);
        }
    }
}
