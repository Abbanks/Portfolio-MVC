using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Entity
{
    public class AdminInfo
    {
        [Key]
        public string AdminInfoId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "";

        public string Title { get; set; } = "";
        public string Email { get; set; } = "";

        public string PhoneNumber { get; set; } = "";
        public string Address { get; set; } = "";

        public string GitHubUrl { get; set; } = "";
        public string LinkedInUrl { get; set; } = "";

        public virtual ICollection<WorkHistory>? WorkHistories { get; set; } = new List<WorkHistory>();
    }
}
