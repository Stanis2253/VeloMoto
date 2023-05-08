using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeloMotoAPI.Migrations
{
    /// <inheritdoc />
    public partial class CorrectlyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_PurchaseInvoice_PruchaseInvoiceId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "PurchaseInvoice");

            migrationBuilder.RenameColumn(
                name: "PruchaseInvoiceId",
                table: "Purchases",
                newName: "PurchaseInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_PruchaseInvoiceId",
                table: "Purchases",
                newName: "IX_Purchases_PurchaseInvoiceId");

            migrationBuilder.CreateTable(
                name: "PurchasesInvoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasesInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasesInvoice_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesInvoice_ProviderId",
                table: "PurchasesInvoice",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_PurchasesInvoice_PurchaseInvoiceId",
                table: "Purchases",
                column: "PurchaseInvoiceId",
                principalTable: "PurchasesInvoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_PurchasesInvoice_PurchaseInvoiceId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "PurchasesInvoice");

            migrationBuilder.RenameColumn(
                name: "PurchaseInvoiceId",
                table: "Purchases",
                newName: "PruchaseInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_PurchaseInvoiceId",
                table: "Purchases",
                newName: "IX_Purchases_PruchaseInvoiceId");

            migrationBuilder.CreateTable(
                name: "PurchaseInvoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoice_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoice_ProviderId",
                table: "PurchaseInvoice",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_PurchaseInvoice_PruchaseInvoiceId",
                table: "Purchases",
                column: "PruchaseInvoiceId",
                principalTable: "PurchaseInvoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
