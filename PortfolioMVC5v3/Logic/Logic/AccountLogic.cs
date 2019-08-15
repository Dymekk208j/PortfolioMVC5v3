using Microsoft.AspNet.Identity.EntityFramework;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models.ViewModels;
using PortfolioMVC5v3.Repositories.Interfaces;
using PortfolioMVC5v3.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioMVC5v3.Logic.Logic
{
    public class AccountLogic : IAccountLogic
    {
        private readonly IAccountRepository _repository;

        public AccountLogic(IAccountRepository repository)
        {
            _repository = repository;
        }

        public Task<List<AppUserViewModel>> GetAllUsers()
        {
            return _repository.GetAllUsers();
        }

        public Task<AppUserViewModel> GetUser(string id)
        {
            return _repository.GetUser(id);
        }

        public Task<List<IdentityRole>> GetAllRoles()
        {
            return _repository.GetAllRoles();
        }

        public Task<IdentityRole> GetRole(string id)
        {
            return _repository.GetRole(id);
        }

        public Task<bool> UpdateRole(IdentityRole role)
        {
            return _repository.UpdateRole(role);
        }

        public Task<bool> AddRole(IdentityRole role)
        {
            role.Id = role.Name;
            return _repository.AddRole(role);
        }

        public Task<bool> RemoveRole(string id)
        {
            return _repository.RemoveRole(id);
        }

        public Task<List<IdentityRole>> GetUserRoles(string id)
        {
            return _repository.GetUserRoles(id);
        }

        public async Task<bool> UpdateUser(AppUserViewModel userViewModel, List<string> rolesIds)
        {
            try
            {
                await _repository.RemoveUserRoles(userViewModel.Id);

                foreach (var roleId in rolesIds)
                {
                    await _repository.CreateBindingBetweenUserAndRole(userViewModel.Id, roleId);
                }

                return await _repository.UpdateUser(userViewModel);
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return false;
        }
    }
}
