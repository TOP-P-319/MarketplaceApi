using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsAPI.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                $"""
                INSERT INTO products(id, name)
                VALUES ('{Guid.CreateVersion7()}', 'Какой-то товар'),
                       ('{Guid.CreateVersion7()}', 'Еще один товар');
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DELETE FROM products AS p
                WHERE p.name = 'Какой-то товар' OR p.name = 'Еще один товар'
                """);
        }
    }
}
