using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models.ViewModel;

namespace Portfolio.Controllers
{
    public class ProfileController : Controller
    {
        private readonly PortfolioDbContext _context;

        public ProfileController(PortfolioDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ViewWorkHistoryViewModel> WorkHistories = new List<ViewWorkHistoryViewModel>();
   
            var workHistories = _context.WorkHistories;

            if (workHistories != null)
            {
                WorkHistories = workHistories.Select(workHistory => new ViewWorkHistoryViewModel
                {
                    CompanyName = workHistory.CompanyName,
                    Position = workHistory.Position,
                    CompanyAddress = workHistory.CompanyAddress,
                    Skills = workHistory.Skills,
                    StartDate = workHistory.StartDate.Date,
                    EndDate = workHistory.EndDate.Date,
                    Description = workHistory.Description
                }).ToList();
               
            }

            var viewModel = new ProfileViewModel
            {
                WorkHistories = WorkHistories.OrderByDescending(w => w.StartDate).ToList()
            };
           
            return View(viewModel);
 
        }


    }
}
