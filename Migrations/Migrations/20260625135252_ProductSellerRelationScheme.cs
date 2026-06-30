using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Shared.Users;

#nullable disable

namespace Migrations.Migrations
{
    /// <inheritdoc />
    public partial class ProductSellerRelationScheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                $"""
                INSERT INTO users (id, name, phone_number, password_hash, balance, role, created_at, updated_at)
                VALUES (
                        '00000000-0000-0000-0000-000000000000',
                        'Продавец',
                        '+79998887766',
                        '',
                        42342340,
                        '{UserRoles.Seller.ToString()}',
                        '{new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)}',
                        '{new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)}'
                        )
                ON CONFLICT (id) DO NOTHING;
                """);
            
            migrationBuilder.AddColumn<Guid>(
                name: "seller_id",
                table: "products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_products_seller_id",
                table: "products",
                column: "seller_id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_users_seller_id",
                table: "products",
                column: "seller_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_users_seller_id",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_seller_id",
                table: "products");

            migrationBuilder.DropColumn(
                name: "seller_id",
                table: "products");
        }
    }
}
