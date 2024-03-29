using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.ViewModel
{
    public class ProfileViewModel
    {
      public IList<ViewWorkHistoryViewModel> WorkHistories { get; set; } 
    }
}
