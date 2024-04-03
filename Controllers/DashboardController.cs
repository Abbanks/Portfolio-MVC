using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.Entity;
using Portfolio.Models.ViewModel;
using Portfolio.Services.Interfaces;

namespace Portfolio.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IGenericEntityRepositoryService<AdminInfo> _adminInfoService;
        private readonly IGenericEntityRepositoryService<WorkHistory> _workHistoryService;

        public DashboardController(IGenericEntityRepositoryService<AdminInfo> adminInfoService,
            IGenericEntityRepositoryService<WorkHistory> workHistoryService)
        {
            _adminInfoService = adminInfoService;
            _workHistoryService = workHistoryService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {

            AdminInfo adminDetails = new AdminInfo();

            var adminInfo = await _adminInfoService.GetAllAsync();
            var workHistories = await _workHistoryService.GetAllAsync();

            if (adminInfo == null || workHistories == null)
            {

                ViewBag.ErrorMessage = $"Admin Information cannot be found";
                return View();
            }

            if (adminInfo != null && workHistories != null && adminInfo.Any() && workHistories.Any())
            {
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
                        Id = workHistory.Id,
                        CompanyName = workHistory.CompanyName,
                        Position = workHistory.Position,
                        CompanyAddress = workHistory.CompanyAddress,
                        Skills = workHistory.Skills,
                        StartDate = workHistory.StartDate,
                        EndDate = workHistory.EndDate,
                        Description = workHistory.Description
                    }).ToList()
                }).First();
            }


            var viewModel = new IndexViewModel
            {
                Name = adminDetails.Name,
                Title = adminDetails.Title,
                Email = adminDetails.Email,
                PhoneNumber = adminDetails.PhoneNumber,
                Address = adminDetails.Address,
                GitHubUrl = adminDetails.GitHubUrl,
                LinkedInUrl = adminDetails.LinkedInUrl,
                WorkHistories = adminDetails.WorkHistories.Select(workHistory => new ViewWorkHistoryViewModel
                {
                    Id = workHistory.Id,
                    CompanyName = workHistory.CompanyName,
                    Position = workHistory.Position,
                    CompanyAddress = workHistory.CompanyAddress,
                    Skills = workHistory.Skills,
                    StartDate = workHistory.StartDate.Date, 
                    EndDate = workHistory.EndDate.Date,
                    Description = workHistory.Description
                }).OrderByDescending(w => w.StartDate).ToList()
            };
            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddAdminInfo()
        {
            AdminInfoViewModel viewModel = new AdminInfoViewModel();

            var adminInfo = _adminInfoService.GetAllAsync().Result.FirstOrDefault();


            if (adminInfo != null)
            {
                viewModel = new AdminInfoViewModel
                {
                    Name = adminInfo.Name,
                    Title = adminInfo.Title,
                    Email = adminInfo.Email,
                    PhoneNumber = adminInfo.PhoneNumber,
                    Address = adminInfo.Address,
                    GitHubUrl = adminInfo.GitHubUrl,
                    LinkedInUrl = adminInfo.LinkedInUrl
                };
            }

            return View(viewModel);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAdminInfo(AdminInfoViewModel model)
        {
            if (ModelState.IsValid)
            {

                var existingAdminInfo = _adminInfoService.GetAllAsync().Result.FirstOrDefault();

                if (existingAdminInfo != null)
                {

                    existingAdminInfo.Name = model.Name;
                    existingAdminInfo.Title = model.Title;
                    existingAdminInfo.Email = model.Email;
                    existingAdminInfo.PhoneNumber = model.PhoneNumber;
                    existingAdminInfo.Address = model.Address;
                    existingAdminInfo.GitHubUrl = model.GitHubUrl;
                    existingAdminInfo.LinkedInUrl = model.LinkedInUrl;

                    await _adminInfoService.UpdateAsync(existingAdminInfo);

                }
                else
                {
                    var newAdminInfo = new AdminInfo
                    {
                        Name = model.Name,
                        Title = model.Title,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        Address = model.Address,
                        GitHubUrl = model.GitHubUrl,
                        LinkedInUrl = model.LinkedInUrl

                    };

                    await _adminInfoService.AddAsync(newAdminInfo);
                }

                TempData["SuccessMessage"] = "Your info has been saved successfully!";

                return RedirectToAction("Index");

            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddWorkHistory()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddWorkHistory(AddWorkHistoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var admins = await _adminInfoService.GetAllAsync();
                if (admins == null || !admins.Any())
                {
                    ViewBag.ErrorMessage = "Admin Information cannot be found";
                    return View();
                }

                var admin = admins.First();

                var newWorkHistory = new WorkHistory
                {
                    CompanyName = model.CompanyName,
                    Position = model.Position,
                    CompanyAddress = model.CompanyAddress,
                    Skills = model.Skills,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Description = model.Description,
                    AdminInfoId = admin.AdminInfoId
                };

                await _workHistoryService.AddAsync(newWorkHistory);

                TempData["SuccessMessage"] = "Your work history has been saved successfully!";
                return RedirectToAction("Index");
            }

            return View(model);
        }


        [HttpGet]
        [Authorize]
        public IActionResult EditWorkHistory(string id)
        {

            var workHistory = _workHistoryService.GetAllAsync().Result.FirstOrDefault(wh => wh.Id == id);

            if (workHistory == null)
            {
                ViewBag.ErrorMessage = "Work History cannot be found";
                return View();
            }


            return View(workHistory);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditWorkHistory(string id, AddWorkHistoryViewModel model)
        {
            var workHistory = _workHistoryService.GetAllAsync().Result.FirstOrDefault(wh => wh.Id == id);

            if (workHistory != null)
            {
                workHistory.CompanyName = model.CompanyName;
                workHistory.Position = model.Position;
                workHistory.CompanyAddress = model.CompanyAddress;
                workHistory.Skills = model.Skills;
                workHistory.StartDate = model.StartDate;
                workHistory.EndDate = model.EndDate;
                workHistory.Description = model.Description;

                await _workHistoryService.UpdateAsync(workHistory);

            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteWorkHistory(string id)
        {

            var workHistory = _workHistoryService.GetAllAsync().Result.FirstOrDefault(wh => wh.Id == id);

            if (workHistory != null)
            {
                await _workHistoryService.DeleteAsync(workHistory);
            }

            return RedirectToAction("Index");
        }


    }
}
