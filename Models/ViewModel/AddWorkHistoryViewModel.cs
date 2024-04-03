using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.ViewModel
{
    public class AddWorkHistoryViewModel
    {
        [Required(ErrorMessage = "Please enter company name.")]
        public string CompanyName { get; set; } = "";

        [Required(ErrorMessage = "Please enter position.")]
        public string Position { get; set; } = "";

        [Required(ErrorMessage = "Please enter job description.")]
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "Please enter company address.")]
        public string CompanyAddress { get; set; } = "";

        [Required(ErrorMessage = "Please enter your skills.")]
        public string Skills { get; set; } = "";

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; } = DateTime.Now.Date;

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; } = DateTime.Now.Date;

        public string? AdminInfoId { get; set; }

    }
}
