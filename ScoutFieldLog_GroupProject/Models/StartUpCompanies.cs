using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScoutFieldLog_GroupProject.Models
{
    public partial class StartUpCompanies
    {
        [Required]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Scout Name")]
        public string ScoutName { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Company Contact Name")]
        public string CompanyContactName { get; set; }

        [Display(Name = "Company Contact Phone Number")]
        public string CompanyContactPhoneNumber { get; set; }

        [Display(Name = "Company Website")]
        public string CompanyWebsite { get; set; }

        [Required]
        [Display(Name = "Two-Line Summary")]
        public string TwoLineSummary { get; set; }

        [Display(Name = "Technology Areas")]
        public string TechnologyAreas { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State / Province")]
        public string StateProvince { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Themes")]
        public string Themes { get; set; }

        [Display(Name = "Landscapes")]
        public string Landscapes { get; set; }

        [Display(Name = "Alignments")]
        public string Alignments { get; set; }

        [Display(Name = "Date Reviewed")]
        public DateTime? DateReviewed { get; set; }

        [Display(Name = "Date Assigned")]
        public DateTime? DateAssigned { get; set; }

        [Display(Name = "Uniqueness")]
        public int? Uniqueness { get; set; }

        [Display(Name = "Team")]
        public int? Team { get; set; }

        [Display(Name = "Raised")]
        public int? Raised { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Keywords")]
        public string Keywords { get; set; }
    }
}
