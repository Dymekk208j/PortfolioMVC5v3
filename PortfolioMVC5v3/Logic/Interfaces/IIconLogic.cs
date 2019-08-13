
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Logic.Interfaces
{
    public interface IIconLogic
    {
        Task<List<Icon>> GetAllIcons();
        Task<Icon> GetIconAsync(int iconId);
        Task<bool> RemoveIconAsync(int iconId);
        Task<int> AddIconAsync(HttpPostedFileBase icon);
        Task<bool> UpdateIconAsync(Icon icon);
    }
}
