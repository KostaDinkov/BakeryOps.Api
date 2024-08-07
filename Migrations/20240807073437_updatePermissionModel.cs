using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.API.Migrations
{
    /// <inheritdoc />
    public partial class updatePermissionModel : Migration
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

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Permission",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permission_Users_UserId",
                table: "Permission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permission",
                table: "Permission");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Permission",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permission",
                table: "Permission",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Permission_Users_UserId",
                table: "Permission",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
