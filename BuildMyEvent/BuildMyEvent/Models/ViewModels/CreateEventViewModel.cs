using System.ComponentModel.DataAnnotations;

namespace BuildMyEvent.Models.ViewModels
{
    public class CreateEventViewModel
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string? LogoFileName { get; set; }

        [Required, MaxLength(50)]
        public string TemplateKey { get; set; } = "default";
    }
}
