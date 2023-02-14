using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.Migrations
{
    /// <inheritdoc />
    public partial class ClientIsSpecialPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSpecialPrice",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSpecialPrice",
                table: "Clients");
        }
    }
}
