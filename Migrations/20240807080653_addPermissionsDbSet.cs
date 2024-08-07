using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.API.Migrations
{
    /// <inheritdoc />
    public partial class addPermissionsDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permission_Users_UserId",
                table: "Permission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permission",
                table: "Permission");

            migrationBuilder.RenameTable(
                name: "Permission",
                newName: "Permissions");

            migrationBuilder.RenameIndex(
                name: "IX_Permission_UserId",
                table: "Permissions",
                newName: "IX_Permissions_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions",
                columns: new[] { "Name", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Users_UserId",
                table: "Permissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Users_UserId",
                table: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions");

            migrationBuilder.RenameTable(
                name: "Permissions",
                newName: "Permission");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_UserId",
                table: "Permission",
                newName: "IX_Permission_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permission",
                table: "Permission",
                columns: new[] { "Name", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Permission_Users_UserId",
                table: "Permission",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
