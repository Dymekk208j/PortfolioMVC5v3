namespace PortfolioMVC5v3.Models
{
    public class Technology
    {
        public int TechnologyId { get; set; }

        public int KnowledgeLevel { get; set; }

        public string Name { get; set; }

        public bool ShowInCv { get; set; }
        public bool ShowInAboutMePage { get; set; }
    }
}
