using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.ViewModel
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Please enter your name.")]
        public string SenderName { get; set; } = "";

        [Required(ErrorMessage = "Please subject.")]
        public string Subject { get; set; } = "";


        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string SenderEmail { get; set; } = "";

        [Required(ErrorMessage = "Please enter a message.")]
        public string Body { get; set; } = "";
    }
}
