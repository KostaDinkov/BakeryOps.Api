using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.API.Migrations
{
    /// <inheritdoc />
    public partial class MaterialsNoVendor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialVendor");

            migrationBuilder.AddColumn<Guid>(
                name: "VendorId",
                table: "Materials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materials_VendorId",
                table: "Materials",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Vendors_VendorId",
                table: "Materials",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Vendors_VendorId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_VendorId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Materials");

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
    }
}
