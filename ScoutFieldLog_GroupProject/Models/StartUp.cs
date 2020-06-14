using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ScoutFieldLog_GroupProject.Models
{
    public partial class StartUp
    {
        public int Id { get; set; }
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [DisplayName("Date Added")]
        public DateTime? DateAdded { get; set; }
        public string Source { get; set; }
        public string Image { get; set; }
        [DisplayName("Company Website")]
        public string CompanyWebsite { get; set; }
        public string City { get; set; }
        [DisplayName("State/Province")]
        public string StateProvince { get; set; }
        public string Country { get; set; }
        [DisplayName("Two-Line Summary")]
        public string TwoLineSummary { get; set; }
        public string ContactId { get; set; }
        public string Themes { get; set; }
        public string Landscapes { get; set; }
        [DisplayName("Technology Areas")]
        public string TechnologyAreas { get; set; }
        public string Alignments { get; set; }
        [DisplayName("Date Assigned")]
        public string DateAssigned { get; set; }
        [DisplayName("Date Saved")]
        public DateTime? DateSaved { get; set; }
        public string Status { get; set; }
    }
}
