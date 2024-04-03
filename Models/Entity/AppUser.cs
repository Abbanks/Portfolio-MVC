using Microsoft.AspNetCore.Identity;

namespace Portfolio.Models.Entity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string ProfilePhoto { get; set; } = "";
    }
}
