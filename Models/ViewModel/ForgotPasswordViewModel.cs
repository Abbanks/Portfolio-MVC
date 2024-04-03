using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

    }
}
