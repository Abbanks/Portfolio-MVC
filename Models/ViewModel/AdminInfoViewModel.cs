using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.ViewModel
{
    public class AdminInfoViewModel
    {
        [Required(ErrorMessage = "Please enter your name.")]
        [StringLength(20)]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Please enter your job title.")]
        [StringLength(20)]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = "";

        [Required(ErrorMessage = "Please enter your address.")]
        public string Address { get; set; } = "";

        [Required(ErrorMessage = "URL is required")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string GitHubUrl { get; set; } = "";

        [Required(ErrorMessage = "URL is required")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string LinkedInUrl { get; set; } = "";



    }
}
