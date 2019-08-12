using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<List<Image>> GetProjectImages(int projectId);
    }
}
