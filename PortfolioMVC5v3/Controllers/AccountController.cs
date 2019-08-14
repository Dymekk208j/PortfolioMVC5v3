using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Models.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PortfolioMVC5v3.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountLogic _accountLogic;
        public AccountController(IAccountLogic accountLogic)
            : this(new UserManager<AppUser>(new UserStore<AppUser>(new ApplicationDbContext())), accountLogic)
        {
            _accountLogic = accountLogic;

        }

        private AccountController(UserManager<AppUser> userManager, IAccountLogic accountLogic)
        {
            UserManager = userManager;
            _accountLogic = accountLogic;
        }

        public async Task<ActionResult> Management()
        {
            var users = await _accountLogic.GetAllUsers();

            return View(users);
        }

        public async Task<ActionResult> RoleManagement()
        {
            var roles = await _accountLogic.GetAllRoles();

            return View(roles);
        }

        public async Task<ActionResult> RoleCard(string id)
        {
            IdentityRole model;
            if (!string.IsNullOrEmpty(id) && !id.Equals("0"))
            {
                model = await _accountLogic.GetRole(id);
            }
            else model = new IdentityRole();

            return View(model);
        }

        public async Task<ActionResult> SaveRole(IdentityRole role)
        {
            bool result;
            if (string.IsNullOrEmpty(role.Id) || role.Id.Equals("0"))
            {
                result = await _accountLogic.AddRole(role);
            }
            else result = await _accountLogic.UpdateRole(role);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }
        public async Task<ActionResult> RemoveRole(string id)
        {
            bool result = await _accountLogic.RemoveRole(id);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        private UserManager<AppUser> UserManager { get; set; }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", @"Błędny login bądź hasło.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Blocked = false
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private async Task SignInAsync(AppUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            ClaimsIdentity identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}