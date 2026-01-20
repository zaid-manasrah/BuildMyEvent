using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildMyEvent.Migrations
{
    /// <inheritdoc />
    public partial class AddCreativeAndDefaultTemplateField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultBodyText",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DefaultBodyTitle",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "DefaultShareNote",
                table: "Events",
                newName: "MinimalInfoTitle");

            migrationBuilder.RenameColumn(
                name: "DefaultCtaText",
                table: "Events",
                newName: "MinimalHeroTitle");

            migrationBuilder.AddColumn<string>(
                name: "AcademicCtaText",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicHeroSubtitle",
                table: "Events",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicHeroTitle",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicShareTitle",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicTrack1Body",
                table: "Events",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicTrack1Title",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicTrack2Body",
                table: "Events",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicTrack2Title",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicTrack3Body",
                table: "Events",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicTrack3Title",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorfulCtaText",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorfulHeroSubtitle",
                table: "Events",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorfulHeroTitle",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorfulHighlight1",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorfulHighlight2",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorfulHighlight3",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MinimalCtaText",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MinimalHeroSubtitle",
                table: "Events",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MinimalInfoBody",
                table: "Events",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademicCtaText",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AcademicHeroSubtitle",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AcademicHeroTitle",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AcademicShareTitle",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AcademicTrack1Body",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AcademicTrack1Title",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AcademicTrack2Body",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AcademicTrack2Title",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AcademicTrack3Body",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AcademicTrack3Title",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ColorfulCtaText",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ColorfulHeroSubtitle",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ColorfulHeroTitle",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ColorfulHighlight1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ColorfulHighlight2",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ColorfulHighlight3",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "MinimalCtaText",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "MinimalHeroSubtitle",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "MinimalInfoBody",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "MinimalInfoTitle",
                table: "Events",
                newName: "DefaultShareNote");

            migrationBuilder.RenameColumn(
                name: "MinimalHeroTitle",
                table: "Events",
                newName: "DefaultCtaText");

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
        }
    }
}
