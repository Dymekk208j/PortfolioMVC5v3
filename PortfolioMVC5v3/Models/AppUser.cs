using Microsoft.AspNet.Identity.EntityFramework;

namespace PortfolioMVC5v3.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
    }
}