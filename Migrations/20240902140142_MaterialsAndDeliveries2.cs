using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.API.Migrations
{
    /// <inheritdoc />
    public partial class MaterialsAndDeliveries2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Materials_MaterialId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Materials_MaterialId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_MaterialId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Products_MaterialId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "MaterialVendor",
                columns: table => new
                {
                    MaterialsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialVendor", x => new { x.MaterialsId, x.VendorsId });
                    table.ForeignKey(
                        name: "FK_MaterialVendor_Materials_MaterialsId",
                        column: x => x.MaterialsId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialVendor_Vendors_VendorsId",
                        column: x => x.VendorsId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialVendor_VendorsId",
                table: "MaterialVendor",
                column: "VendorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialVendor");

            migrationBuilder.AddColumn<Guid>(
                name: "MaterialId",
                table: "Vendors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MaterialId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_MaterialId",
                table: "Vendors",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MaterialId",
                table: "Products",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Materials_MaterialId",
                table: "Products",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Materials_MaterialId",
                table: "Vendors",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id");
        }
    }
}
