using Microsoft.AspNetCore.Mvc;
using Portfolio.Data;
 
using Portfolio.Models.Entity;
using Portfolio.Models.ViewModel;
using Portfolio.Services.Interfaces;

namespace Portfolio.Controllers
{
    public class ContactController : Controller
    {
        private readonly PortfolioDbContext _context;

        private readonly IEmailService _emailService;
        public ContactController(PortfolioDbContext context, IEmailService emailService)
        {
            _context = context;
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

                _context.Emails.Add(message);
                _context.SaveChanges();

 

                await _emailService.SendEmail(model.SenderName, model.Subject, model.SenderEmail, model.Body);
                ViewData["SuccessMessage"] = "Your message has been sent successfully!";

                return RedirectToAction("Contact");

            }

            return View(model);
        }

       

    }
}
