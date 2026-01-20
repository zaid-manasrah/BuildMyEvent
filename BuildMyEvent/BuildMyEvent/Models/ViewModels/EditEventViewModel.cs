using System.ComponentModel.DataAnnotations;

namespace BuildMyEvent.Models.ViewModels
{
    public class EditEventViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string? LogoFileName { get; set; }

        [Required, MaxLength(100)]
        public string Slug { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string TemplateKey { get; set; } = "default";

        [MaxLength(200)]
        public string? TechProWhyAttendTitle { get; set; }

        [MaxLength(300)]
        public string? TechProWhyAttendPoint1 { get; set; }

        [MaxLength(300)]
        public string? TechProWhyAttendPoint2 { get; set; }

        [MaxLength(300)]
        public string? TechProWhyAttendPoint3 { get; set; }

        [MaxLength(200)]
        public string? TechProCard1Title { get; set; }

        [MaxLength(500)]
        public string? TechProCard1Body { get; set; }

        [MaxLength(200)]
        public string? TechProCard2Title { get; set; }

        [MaxLength(500)]
        public string? TechProCard2Body { get; set; }

        [MaxLength(200)]
        public string? TechProCard3Title { get; set; }

        [MaxLength(500)]
        public string? TechProCard3Body { get; set; }

        [MaxLength(200)]
        public string? CreativeHeroTitle { get; set; }

        [MaxLength(400)]
        public string? CreativeHeroSubtitle { get; set; }

        [MaxLength(100)]
        public string? CreativeCtaText { get; set; }

        [MaxLength(200)]
        public string? CreativeShareTitle { get; set; }

        [MaxLength(200)]
        public string? BusinessHeroBadge { get; set; }

        [MaxLength(200)]
        public string? BusinessHeroTitle { get; set; }

        [MaxLength(400)]
        public string? BusinessHeroSubtitle { get; set; }

        [MaxLength(100)]
        public string? BusinessCtaText { get; set; }

        [MaxLength(200)]
        public string? BusinessCard1Title { get; set; }

        [MaxLength(400)]
        public string? BusinessCard1Body { get; set; }

        [MaxLength(200)]
        public string? BusinessCard2Title { get; set; }

        [MaxLength(400)]
        public string? BusinessCard2Body { get; set; }

        [MaxLength(200)]
        public string? BusinessCard3Title { get; set; }

        [MaxLength(400)]
        public string? BusinessCard3Body { get; set; }

        [MaxLength(200)]
        public string? BusinessShareTitle { get; set; }

        [MaxLength(200)]
        public string? MinimalHeroTitle { get; set; }

        [MaxLength(400)]
        public string? MinimalHeroSubtitle { get; set; }

        [MaxLength(100)]
        public string? MinimalCtaText { get; set; }

        [MaxLength(200)]
        public string? MinimalInfoTitle { get; set; }

        [MaxLength(400)]
        public string? MinimalInfoBody { get; set; }

        [MaxLength(200)]
        public string? ColorfulHeroTitle { get; set; }

        [MaxLength(400)]
        public string? ColorfulHeroSubtitle { get; set; }

        [MaxLength(100)]
        public string? ColorfulCtaText { get; set; }

        [MaxLength(200)]
        public string? ColorfulHighlight1 { get; set; }

        [MaxLength(200)]
        public string? ColorfulHighlight2 { get; set; }

        [MaxLength(200)]
        public string? ColorfulHighlight3 { get; set; }

        [MaxLength(200)]
        public string? AcademicHeroTitle { get; set; }

        [MaxLength(400)]
        public string? AcademicHeroSubtitle { get; set; }

        [MaxLength(100)]
        public string? AcademicCtaText { get; set; }

        [MaxLength(200)]
        public string? AcademicTrack1Title { get; set; }

        [MaxLength(400)]
        public string? AcademicTrack1Body { get; set; }

        [MaxLength(200)]
        public string? AcademicTrack2Title { get; set; }

        [MaxLength(400)]
        public string? AcademicTrack2Body { get; set; }

        [MaxLength(200)]
        public string? AcademicTrack3Title { get; set; }

        [MaxLength(400)]
        public string? AcademicTrack3Body { get; set; }

        [MaxLength(200)]
        public string? AcademicShareTitle { get; set; }
    }
}
