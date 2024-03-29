namespace Portfolio.Models.ViewModel
{
    public class IndexViewModel
    {

        public string Name { get; set; } = "";
        public string Title { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";

        public string Address { get; set; } = "";
        public string GitHubUrl { get; set; } = "";
        public string LinkedInUrl { get; set; } = "";


        public ICollection<ViewWorkHistoryViewModel> WorkHistories { get; set; }
    }
}
