using System;
using System.Collections.Generic;

namespace ScoutFieldLog_GroupProject.Models
{
    public partial class StartUp
    {       
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public DateTime? DateAdded { get; set; }
        public string Source { get; set; }
        public string Image { get; set; }
        public string CompanyWebsite { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public string TwoLineSummary { get; set; }
        public string ContactId { get; set; }
        public string Themes { get; set; }
        public string Landscapes { get; set; }
        public string TechnologyAreas { get; set; }
        public string Alignments { get; set; }
        public string DateAssigned { get; set; }
        public DateTime? DateSaved { get; set; }
        public string Status { get; set; }
    }
}
