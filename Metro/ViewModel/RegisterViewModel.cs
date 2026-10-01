using System.ComponentModel.DataAnnotations;

namespace MetroApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "National ID must be 14 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "National ID must contain numbers only.")]
        public string NationalId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Profile Picture (Optional)")]
        public IFormFile? ProfilePicture { get; set; }
    }
}
