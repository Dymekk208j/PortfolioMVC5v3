using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Repositories.Interfaces
{
    public interface IIconRepository
    {
        Task<List<Icon>> GetAllIcons();
        Task<Icon> GetIconAsync(int iconId);
        Task<bool> RemoveIconAsync(int iconId);
        Task<bool> AddIconAsync(Icon icon);
        Task<bool> UpdateIconAsync(Icon icon);
    }
}
