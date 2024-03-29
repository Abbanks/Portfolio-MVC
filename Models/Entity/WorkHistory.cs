using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Entity
{
    public class WorkHistory
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CompanyName { get; set; } = "";
        public string Position { get; set; } = "";
        public string Description { get; set; } = "";

        public string CompanyAddress { get; set; } = "";

        public string Skills { get; set; } = ""; 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }  

        public string AdminInfoId { get; set; } = "";
        public AdminInfo? AdminInfo { get; set; } 
    }
}
