using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildMyEvent.Migrations
{
    /// <inheritdoc />
    public partial class AddCreativeAndDefaultTemplateFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreativeCtaText",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreativeHeroSubtitle",
                table: "Events",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreativeHeroTitle",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreativeShareTitle",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultBodyText",
                table: "Events",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultBodyTitle",
                table: "Events",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultCtaText",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultShareNote",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreativeCtaText",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreativeHeroSubtitle",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreativeHeroTitle",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreativeShareTitle",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DefaultBodyText",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DefaultBodyTitle",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DefaultCtaText",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DefaultShareNote",
                table: "Events");
        }
    }
}
