using Microsoft.AspNetCore.Mvc;

using Portfolio.Models.Entity;
using Portfolio.Models.ViewModel;
using Portfolio.Services.Interfaces;

namespace Portfolio.Controllers
{
    public class ContactController : Controller
    {
        private readonly IGenericEntityRepositoryService<Email> _emailRepositoryService;

        private readonly IEmailService _emailService;
        public ContactController(IGenericEntityRepositoryService<Email> emailRepositoryService,
            IEmailService emailService)
        {
            _emailRepositoryService = emailRepositoryService;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ActionName("contact")]
        public async Task<IActionResult> SaveContactMessage(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {

                var message = new Email
                {
                    SenderName = model.SenderName,
                    Subject = model.Subject,
                    SenderEmail = model.SenderEmail,
                    Body = model.Body
                };

                await _emailRepositoryService.AddAsync(message);

                await _emailService.SendEmail(model.SenderName, model.Subject, model.SenderEmail, model.Body);

                return RedirectToAction("Contact");

            }

            return View(model);
        }



    }
}
