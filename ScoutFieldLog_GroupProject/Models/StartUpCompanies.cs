using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScoutFieldLog_GroupProject.Models
{
    public partial class StartUpCompanies
    {
        public int Id { get; set; }
        [Required]
        public string ScoutName { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string CompanyContactName { get; set; }
        public string CompanyContactPhoneNumber { get; set; }
        public string CompanyWebsite { get; set; }
        [Required]
        public string TwoLineSummary { get; set; }
        public string TechnologyAreas { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public string Themes { get; set; }
        public string Landscapes { get; set; }
        public string Alignments { get; set; }
        public DateTime? DateReviewed { get; set; }
        public DateTime? DateAssigned { get; set; }
        public int? Uniqueness { get; set; }
        public int? Team { get; set; }
        public int? Raised { get; set; }
        public string Status { get; set; }
        public string Keywords { get; set; }
    }
}
