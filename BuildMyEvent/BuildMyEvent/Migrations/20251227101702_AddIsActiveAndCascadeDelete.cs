using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildMyEvent.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveAndCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_OwnerUserId",
                table: "Events");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_OwnerUserId",
                table: "Events",
                column: "OwnerUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_OwnerUserId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_OwnerUserId",
                table: "Events",
                column: "OwnerUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
