using System.Collections.Generic;

namespace PortfolioMVC5v3.Models.ViewModels
{
    public class CvViewModel
    {
        public PersonalDataViewModel PersonalDataViewModel { get; set; }
        public ContactDataViewModel ContactDataViewModel { get; set; }
        public List<Achievement> Achievements { get; set; }
        public List<Education> Educations { get; set; }
        public List<EmploymentHistory> EmploymentHistories { get; set; }
        public List<ExtraInformation> ExtraInformation { get; set; }
        public List<ProjectViewModel> Projects { get; set; }
        public List<Technology> Technologies { get; set; }
    }

    public class PersonalDataViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string PersonalPhotoLink { get; set; }
    }

    public class ContactDataViewModel
    {
        public string PhoneNumber { get; set; }
        public string HomePageLink { get; set; }
        public string GitHubLink { get; set; }
        public string EmailAddress { get; set; }
    }
}