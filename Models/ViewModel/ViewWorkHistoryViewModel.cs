using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Portfolio.Models.ViewModel
{
    public class ViewWorkHistoryViewModel
    {
        public string Id { get; set; } 
        public string CompanyName { get; set; } = "";
        public string Position { get; set; } = "";
        public string Description { get; set; } = "";
        public string CompanyAddress { get; set; } = "";
        public string Skills { get; set; } = "";
        public DateTime StartDate { get; set; } = DateTime.Now.Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
    }
}
