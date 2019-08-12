using System;

namespace PortfolioMVC5v3.Models
{
    public class Achievement
    {
        public int AchievementId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool ShowInCv { get; set; }
    }
}