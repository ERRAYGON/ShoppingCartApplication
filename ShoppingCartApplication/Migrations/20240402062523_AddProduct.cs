using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCartApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "User",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            string AddProduct = @"
            CREATE PROCEDURE AddProduct
                @ProductId uniqueidentifier,
                @ProductName nvarchar(255),
                @ProductPrice decimal(18,2)
            AS
            BEGIN
                INSERT INTO Products(ProductId, ProductName, ProductPrice)
                VALUES (@ProductId, @ProductName, @ProductPrice)
            END";
            migrationBuilder.Sql(AddProduct);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);
        }
    }
}
