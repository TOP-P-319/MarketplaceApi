using Microsoft.EntityFrameworkCore.Migrations;
using ProductsAPI.Core.Utils.Db;

#nullable disable

namespace ProductsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedAtTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.FunctionMigrations.UpdatedAt.Add();
            migrationBuilder.TriggerMigrations.UpdatedAt.AddToTable("products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.FunctionMigrations.UpdatedAt.Remove();
            migrationBuilder.TriggerMigrations.UpdatedAt.RemoveFromTable("products");
        }
    }
}
