using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildMyEvent.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplateKeyToEvents1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TechProCard1Body",
                table: "Events",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechProCard1Title",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechProCard2Body",
                table: "Events",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechProCard2Title",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechProCard3Body",
                table: "Events",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechProCard3Title",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechProWhyAttendPoint1",
                table: "Events",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechProWhyAttendPoint2",
                table: "Events",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechProWhyAttendPoint3",
                table: "Events",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechProWhyAttendTitle",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TechProCard1Body",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TechProCard1Title",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TechProCard2Body",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TechProCard2Title",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TechProCard3Body",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TechProCard3Title",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TechProWhyAttendPoint1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TechProWhyAttendPoint2",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TechProWhyAttendPoint3",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TechProWhyAttendTitle",
                table: "Events");
        }
    }
}
