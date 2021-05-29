using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MST_Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "MST_Payment_Mode",
                columns: table => new
                {
                    PaymentModeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentsMode = table.Column<string>(maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_Payment_Mode", x => x.PaymentModeId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(nullable: false),
                    PaymentModeId = table.Column<int>(nullable: false),
                    TxnDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    TransactionType = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_MST_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "MST_Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_MST_Payment_Mode_PaymentModeId",
                        column: x => x.PaymentModeId,
                        principalTable: "MST_Payment_Mode",
                        principalColumn: "PaymentModeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentModeId",
                table: "Transactions",
                column: "PaymentModeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "MST_Category");

            migrationBuilder.DropTable(
                name: "MST_Payment_Mode");
        }
    }
}
