using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixValueGenerator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "products",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("93f8a9cc-85df-48a1-8b82-4b8808f3ca50"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("93f8a9cc-85df-48a1-8b82-4b8808f3ca50"),
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
