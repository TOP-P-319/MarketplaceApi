using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations.Migrations
{
    /// <inheritdoc />
    public partial class RenamePurchaseScheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_users_buyer_id",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_users_seller_id",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.RenameTable(
                name: "Purchases",
                newName: "purchases");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_seller_id",
                table: "purchases",
                newName: "IX_purchases_seller_id");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_buyer_id",
                table: "purchases",
                newName: "IX_purchases_buyer_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_purchases",
                table: "purchases",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_purchases_users_buyer_id",
                table: "purchases",
                column: "buyer_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_purchases_users_seller_id",
                table: "purchases",
                column: "seller_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchases_users_buyer_id",
                table: "purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_purchases_users_seller_id",
                table: "purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_purchases",
                table: "purchases");

            migrationBuilder.RenameTable(
                name: "purchases",
                newName: "Purchases");

            migrationBuilder.RenameIndex(
                name: "IX_purchases_seller_id",
                table: "Purchases",
                newName: "IX_Purchases_seller_id");

            migrationBuilder.RenameIndex(
                name: "IX_purchases_buyer_id",
                table: "Purchases",
                newName: "IX_Purchases_buyer_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_users_buyer_id",
                table: "Purchases",
                column: "buyer_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_users_seller_id",
                table: "Purchases",
                column: "seller_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
