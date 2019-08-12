using System;
using System.Collections.Generic;

namespace PortfolioMVC5v3.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public bool Commercial { get; set; }

        public bool ShowInCv { get; set; }
        
        public DateTime DateTimeCreated { get; set; }

        public string AuthorId { get; set; }

        public bool TempProject { get; set; }

        public string GitHubLink { get; set; }

        public List<Technology> Technologies { get; set; }

        //public Image Icon { get; set; }
        //public virtual List<Image> Images { get; set; }
    }
}
