using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.Entity;
using Portfolio.Models.ViewModel;
using Portfolio.Services.Interfaces;

namespace Portfolio.Controllers
{
    public class ProfileController : Controller
    {

        private readonly IGenericEntityRepositoryService<WorkHistory> _workHistoryService;
        public ProfileController(IGenericEntityRepositoryService<WorkHistory> workHistoryService)
        {
            _workHistoryService = workHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ViewWorkHistoryViewModel> WorkHistories = new List<ViewWorkHistoryViewModel>();

            var workHistories = await _workHistoryService.GetAllAsync();

            if (workHistories != null)
            {
                WorkHistories = workHistories.Select(workHistory => new ViewWorkHistoryViewModel
                {
                    CompanyName = workHistory.CompanyName,
                    Position = workHistory.Position,
                    CompanyAddress = workHistory.CompanyAddress,
                    Skills = workHistory.Skills,
                    StartDate = workHistory.StartDate.Date,
                    EndDate = workHistory.EndDate,
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
