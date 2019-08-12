using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Repositories.Interfaces;

namespace PortfolioMVC5v3.Logic.Logic
{
    public class ImageLogic : IImageLogic
    {
        private readonly IImageRepository _repository;

        public ImageLogic(IImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Image>> GetProjectImages(int projectId)
        {
            return await _repository.GetProjectImages(projectId);
        }
    }
}
