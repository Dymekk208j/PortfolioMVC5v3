using System;

namespace PortfolioMVC5v3.Models
{
    public class EmploymentHistory
    {
        public int EmploymentHistoryId { get; set; }
        public string CompanyName { get; set; }
        public string CityOfEmployment { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CurrentPlaceOfEmployment { get; set; }
        public bool ShowInCv { get; set; }
    }
}