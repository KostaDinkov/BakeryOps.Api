using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.Migrations
{
    /// <inheritdoc />
    public partial class SeparateDateTimeInOrderNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PickupTime",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickupTime",
                table: "Orders");
        }
    }
}
