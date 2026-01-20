using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildMyEvent.Migrations
{
    /// <inheritdoc />
    public partial class AddCreativeAndDefaultTemplateFields1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusinessCard1Body",
                table: "Events",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessCard1Title",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessCard2Body",
                table: "Events",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessCard2Title",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessCard3Body",
                table: "Events",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessCard3Title",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessCtaText",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessHeroBadge",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessHeroSubtitle",
                table: "Events",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessHeroTitle",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessShareTitle",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessCard1Body",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BusinessCard1Title",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BusinessCard2Body",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BusinessCard2Title",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BusinessCard3Body",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BusinessCard3Title",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BusinessCtaText",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BusinessHeroBadge",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BusinessHeroSubtitle",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BusinessHeroTitle",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BusinessShareTitle",
                table: "Events");
        }
    }
}
