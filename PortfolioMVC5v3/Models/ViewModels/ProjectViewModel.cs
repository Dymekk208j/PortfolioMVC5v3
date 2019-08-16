using System.Collections.Generic;

namespace PortfolioMVC5v3.Models.ViewModels
{
    public class ProjectViewModel : Project
    {
        public Icon Icon { get; set; }
        public List<Technology> Technologies { get; set; }
        public List<Image> Images { get; set; }
    }
}