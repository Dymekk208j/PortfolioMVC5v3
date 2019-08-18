using Newtonsoft.Json;
using PortfolioMVC5v3.Logic.Interfaces;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PortfolioMVC5v3.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class IconController : Controller
    {
        private readonly IIconLogic _iconLogic;

        public IconController(IIconLogic iconLogic)
        {
            _iconLogic = iconLogic;
        }

        public async Task<ActionResult> Management()
        {
            var icons = await _iconLogic.GetAllIcons();

            return View(icons);
        }

        public ActionResult AddIcon()
        {
            return View();
        }

        public async Task<ActionResult> UploadIcon(HttpPostedFileBase iconFile)
        {
            await _iconLogic.AddIconAsync(iconFile);

            return RedirectToAction("Management");
        }

        public async Task<ActionResult> RemoveIcon(int id)
        {
            var result = await _iconLogic.RemoveIconAsync(id);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<string> GetIcons()
        {
            var icons = await _iconLogic.GetAllIcons();
            return JsonConvert.SerializeObject(icons);
        }

    }
}