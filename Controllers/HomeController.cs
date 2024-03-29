using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;
using Portfolio.Models.Entity;
using Portfolio.Models.ViewModel;
using System.Diagnostics;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly PortfolioDbContext _context;

        public HomeController(PortfolioDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            
            AdminInfo adminDetails = new AdminInfo();

            var adminInfo = _context.AdminInfos;
            var workHistories = _context.WorkHistories;

            if (adminInfo != null && workHistories != null && adminInfo.Any() && workHistories.Any()) { 
             adminDetails = adminInfo.Join(workHistories, a => a.AdminInfoId, w => w.AdminInfoId, (a, w) => new { a, w }).Select(x => new AdminInfo
            {
                Name = x.a.Name,
                Title = x.a.Title,
                Email = x.a.Email,
                PhoneNumber = x.a.PhoneNumber,
                Address = x.a.Address,
                GitHubUrl = x.a.GitHubUrl,
                LinkedInUrl = x.a.LinkedInUrl,
                WorkHistories = x.a.WorkHistories.Select(workHistory => new WorkHistory
                {
                    CompanyName = workHistory.CompanyName,
                    Position = workHistory.Position,
                    CompanyAddress = workHistory.CompanyAddress,
                    StartDate = workHistory.StartDate.Date,
                }).ToList()
            }).First();
        }
        

            var viewModel = new HomeViewModel
            {
                Name = adminDetails.Name,
                Title = adminDetails.Title,
                Email = adminDetails.Email,
                PhoneNumber = adminDetails.PhoneNumber,
                Address = adminDetails.Address,
                GitHubUrl = adminDetails.GitHubUrl,
                LinkedInUrl = adminDetails.LinkedInUrl,
                WorkHistories = adminDetails.WorkHistories.Select(workHistory => new AddWorkHistoryViewModel
                {
                    CompanyName = workHistory.CompanyName,
                    Position = workHistory.Position,
                    CompanyAddress = workHistory.CompanyAddress,
                    StartDate = workHistory.StartDate.Date
                }).OrderByDescending(w => w.StartDate).ToList()
            };
             return View(viewModel);
           

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
