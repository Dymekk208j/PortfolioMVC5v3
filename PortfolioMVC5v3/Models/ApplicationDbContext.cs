using Microsoft.AspNet.Identity.EntityFramework;

namespace PortfolioMVC5v3.Models
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext()
            : base("AppConnectionString", false)
        {
            Database.CommandTimeout = 900;

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public new virtual int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}