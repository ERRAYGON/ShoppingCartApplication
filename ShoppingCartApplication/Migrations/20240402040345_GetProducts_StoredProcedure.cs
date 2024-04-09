using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCartApplication.Migrations
{
    /// <inheritdoc />
    public partial class GetProducts_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string GetAllProducts = @"
            CREATE PROCEDURE [dbo].[GetAllProducts]
            AS BEGIN
                SELECT * FROM [dbo].[Products]
            END
            ";
            migrationBuilder.Sql(GetAllProducts);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string GetAllProducts = @"
            DROP PROCEDURE [dbo].[GetAllProducts]
            ";
            migrationBuilder.Sql(GetAllProducts);
        }
    }
}
