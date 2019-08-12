namespace PortfolioMVC5v3.Models
{
    public class ExtraInformation
    {
        public int ExtraInformationId { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public bool ShowInCv { get; set; }
    }
}