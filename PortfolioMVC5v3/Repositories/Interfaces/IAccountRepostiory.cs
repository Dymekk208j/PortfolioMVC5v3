using Microsoft.AspNet.Identity.EntityFramework;
using PortfolioMVC5v3.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioMVC5v3.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<AppUserViewModel>> GetAllUsers();
        Task<AppUserViewModel> GetUser(string id);
        Task<List<IdentityRole>> GetAllRoles();
        Task<IdentityRole> GetRole(string id);
        Task<bool> UpdateRole(IdentityRole role);
        Task<bool> AddRole(IdentityRole role);
        Task<bool> RemoveRole(string id);
        Task<List<IdentityRole>> GetUserRoles(string id);
        Task<bool> RemoveUserRoles(string id);
        Task<bool> CreateBindingBetweenUserAndRole(string id, string roleId);
        Task<bool> UpdateUser(AppUserViewModel userViewModel);
    }
}
