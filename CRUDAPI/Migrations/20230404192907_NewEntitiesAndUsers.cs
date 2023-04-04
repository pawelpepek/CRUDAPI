using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRUDAPI.Migrations
{
    public partial class NewEntitiesAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Client_ClientId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAmount_Order_OrderId",
                table: "ProductAmount");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAmount_Products_ProductId",
                table: "ProductAmount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAmount",
                table: "ProductAmount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.RenameTable(
                name: "ProductAmount",
                newName: "ProductAmounts");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAmount_ProductId",
                table: "ProductAmounts",
                newName: "IX_ProductAmounts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAmount_OrderId",
                table: "ProductAmounts",
                newName: "IX_ProductAmounts_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAmounts",
                table: "ProductAmounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Clients_ClientId",
                table: "Order",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAmounts_Order_OrderId",
                table: "ProductAmounts",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAmounts_Products_ProductId",
                table: "ProductAmounts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Clients_ClientId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAmounts_Order_OrderId",
                table: "ProductAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAmounts_Products_ProductId",
                table: "ProductAmounts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAmounts",
                table: "ProductAmounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "ProductAmounts",
                newName: "ProductAmount");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAmounts_ProductId",
                table: "ProductAmount",
                newName: "IX_ProductAmount_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAmounts_OrderId",
                table: "ProductAmount",
                newName: "IX_ProductAmount_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAmount",
                table: "ProductAmount",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Client_ClientId",
                table: "Order",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAmount_Order_OrderId",
                table: "ProductAmount",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAmount_Products_ProductId",
                table: "ProductAmount",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
