using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Repositories.Interfaces;
using PortfolioMVC5v3.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PortfolioMVC5v3.Logic.Logic
{
    public class IconLogic : IIconLogic
    {
        private readonly IIconRepository _repository;

        public IconLogic(IIconRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Icon>> GetAllIcons()
        {
            return _repository.GetAllIcons();
        }

        public Task<Icon> GetIconAsync(int iconId)
        {
            return _repository.GetIconAsync(iconId);
        }

        public async Task<bool> RemoveIconAsync(int iconId)
        {
            var icon = await _repository.GetIconAsync(iconId);
            string serverFolderPath = HttpContext.Current.Server.MapPath("~/Assets/Icons/");
            var fullPath = $"{serverFolderPath}{icon.Guid}{icon.FileName}";

            if (!File.Exists(fullPath)) return false;

            File.Delete(fullPath);
            return await _repository.RemoveIconAsync(iconId);
        }

        public async Task<int> AddIconAsync(HttpPostedFileBase icon)
        {
            string[] allowedFormats = { ".jpg", ".png", ".gif", ".jpeg" };
            var isAllowedFormat = allowedFormats.Any(item => icon.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
            if (!isAllowedFormat) return -1;

            string fileName = Path.GetFileNameWithoutExtension(icon.FileName);
            string filExtension = Path.GetExtension(icon.FileName);
            string guid = Guid.NewGuid().ToString();
            string path = HttpContext.Current.Server.MapPath("~/Assets/Icons/");


            try
            {
                icon.SaveAs($"{path}{guid}{fileName}{filExtension}");
                Icon iconModel = new Icon()
                {
                    FileName = fileName + filExtension,
                    Guid = guid
                };

                return await _repository.AddIconAsync(iconModel);
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return -1;
            }
        }

        public Task<bool> UpdateIconAsync(Icon icon)
        {
            return _repository.UpdateIconAsync(icon);
        }
    }
}
