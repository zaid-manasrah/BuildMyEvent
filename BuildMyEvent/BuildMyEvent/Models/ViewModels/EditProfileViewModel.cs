using System.ComponentModel.DataAnnotations;

namespace BuildMyEvent.Models.ViewModels
{
    public class EditProfileViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; } = string.Empty;
    }
}
