using System;

namespace PortfolioMVC5v3.Models
{
    public class Education
    {
        public int EducationId { get; set; }
        public string SchoolName { get; set; }
        public string Department { get; set; }
        public string Specialization { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CurrentPlaceOfEducation { get; set; }
        public bool ShowInCv { get; set; }
    }
}