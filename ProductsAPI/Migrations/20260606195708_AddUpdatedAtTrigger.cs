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
            migrationBuilder.CreateUpdatedAtFunction();
            migrationBuilder.AddUpdatedAtTriggerToTable("products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RemoveUpdatedAtFunction();
            migrationBuilder.RemoveUpdatedAtTriggerFromTable("products");
        }
    }
}
