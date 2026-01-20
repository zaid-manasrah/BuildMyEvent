using System.ComponentModel.DataAnnotations;

namespace BuildMyEvent.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required, MaxLength(200)]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;
    }
}
