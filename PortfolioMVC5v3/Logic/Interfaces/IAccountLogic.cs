using Microsoft.AspNet.Identity.EntityFramework;
using PortfolioMVC5v3.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioMVC5v3.Logic.Interfaces
{
    public interface IAccountLogic
    {
        Task<List<AppUserViewModel>> GetAllUsers();
        Task<List<IdentityRole>> GetAllRoles();
        Task<IdentityRole> GetRole(string id);
        Task<bool> UpdateRole(IdentityRole role);
        Task<bool> AddRole(IdentityRole role);
        Task<bool> RemoveRole(string id);
    }
}
