using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models.Entity;
using Portfolio.Models.ViewModel;
using System.Linq;

namespace Portfolio.Controllers
{
    public class DashboardController : Controller
    {
       
        private readonly PortfolioDbContext _context;
        public DashboardController(PortfolioDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            
            AdminInfo adminDetails = new AdminInfo();

            var adminInfo = _context.AdminInfos;
            var workHistories = _context.WorkHistories;

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
                        StartDate = workHistory.StartDate.Date,
                        EndDate = workHistory.EndDate.Date,
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
        public IActionResult AddAdminInfo()
        {
            AdminInfoViewModel viewModel = new AdminInfoViewModel();

            var adminInfo = _context.AdminInfos.FirstOrDefault();

           
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
        public IActionResult AddAdminInfo(AdminInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var adminInfo = _context.AdminInfos;

                var existingAdminInfo = adminInfo.FirstOrDefault();
            
                if (existingAdminInfo != null)
                {
                   // _context.Entry(existingAdminInfo).Reload(); // Reload the entity from the database

                    existingAdminInfo.Name = model.Name;
                    existingAdminInfo.Title = model.Title;
                    existingAdminInfo.Email = model.Email;
                    existingAdminInfo.PhoneNumber = model.PhoneNumber;
                    existingAdminInfo.Address = model.Address;
                    existingAdminInfo.GitHubUrl = model.GitHubUrl;
                    existingAdminInfo.LinkedInUrl = model.LinkedInUrl;

                     
                    adminInfo.Update(existingAdminInfo);
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
                 
                    adminInfo.Add(newAdminInfo);
                }

                _context.SaveChanges();

                TempData["SuccessMessage"] = "Your info has been saved successfully!";

                return RedirectToAction("Index");
             
            }
            return View(model);
        }

        /*  [HttpGet]
          public IActionResult ClearSuccessMessage()
          {
              TempData.Remove("SuccessMessage");
              return RedirectToAction("AddAdminInfo");
          }*/

        [HttpGet]
        public IActionResult AddWorkHistory()
        {
            return View();
        }   

        [HttpPost]
        public IActionResult AddWorkHistory(AddWorkHistoryViewModel model)
        {
            var admins = _context.AdminInfos;
            if(admins == null)
            {
                ViewBag.ErrorMessage = "Admin Information cannot be found";
                return View();
            }   

            var admin = admins.First();

            if (ModelState.IsValid)
            {
                var newWorkHistory = new WorkHistory
                {
                    //Id = Guid.NewGuid().ToString(),
                    CompanyName = model.CompanyName,
                    Position = model.Position,
                    CompanyAddress = model.CompanyAddress,
                    Skills = model.Skills,
                    StartDate = model.StartDate.Date,
                    EndDate = model.EndDate.Date,
                    Description = model.Description,
                    AdminInfoId = admin.AdminInfoId
                };

           
                _context.WorkHistories.Add(newWorkHistory);

                _context.SaveChanges();

                TempData["SuccessMessage"] = "Your work history has been saved successfully!";

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EditWorkHistory(string id)
        {
            //AddWorkHistoryViewModel viewModel = new AddWorkHistoryViewModel();

            var workHistories = _context.WorkHistories;
            var workHistory = workHistories.FirstOrDefault(wh => wh.Id == id);

            if (workHistory == null)
            {
                ViewBag.ErrorMessage = "Work History cannot be found";
                return View();
            }

             

            return View(workHistory);
        }

        [HttpPost]
        public IActionResult EditWorkHistory(string id, AddWorkHistoryViewModel model)
        {
            var workHistory = _context.WorkHistories.FirstOrDefault(wh => wh.Id == id);

            if (workHistory != null) 
            {
                workHistory.CompanyName = model.CompanyName;
                workHistory.Position = model.Position;
                workHistory.CompanyAddress = model.CompanyAddress;
                workHistory.Skills = model.Skills;
                workHistory.StartDate = model.StartDate.Date;
                workHistory.EndDate = model.EndDate.Date;
                workHistory.Description = model.Description;

                _context.WorkHistories.Update(workHistory);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult DeleteWorkHistory(string id)
        {
 
            var workHistory = _context.WorkHistories.FirstOrDefault(wh => wh.Id == id);

            if (workHistory != null)
            {
                _context.Remove(workHistory);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteAdminInfo()
        {
            var adminInfo = _context.AdminInfos;
            var admin = adminInfo.FirstOrDefault();

            if (admin != null)
            {
                adminInfo.Remove(admin);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
