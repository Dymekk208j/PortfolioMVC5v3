using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Logic.Interfaces
{
    public interface IImageLogic
    {
        Task<List<Image>> GetProjectImages(int projectId);
    }
}
