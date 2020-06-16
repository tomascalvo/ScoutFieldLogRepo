using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

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
        //[NotMapped]
        //public List<Alignment> Alignments { get; set; }
        public string Alignments { get; set; }
        [DisplayName("Date Assigned")]
        public string DateAssigned { get; set; }
        [DisplayName("Date Saved")]
        public DateTime? DateSaved { get; set; }
        public string Status { get; set; }
        public List<Evaluation> Evaluations { get; set; }
        //[DisplayName("Partner Company")]
        //public string PartnerCompany { get; set; }
    }
    public class Evaluation
    {
        public int Id { get; set; }
        public int StartUpId { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatorName { get; set; }
        public List<Alignment> Alignments { get; set; }
        public int Uniqueness { get; set; }
        public int TeamStrength { get; set; }
        public int BusinessPotential { get; set; }
        public int LevelOfInterest { get; set; }
        public string Body { get; set; }
    }
    public class Alignment
    {
        public int Id { get; set; }
        public string PartnerName { get; set; }
        public int Score { get; set; }
    }
    // Parent view model that contains StartUp and Evaluation for CreateEvaluation view.
    //    public class CreateEvaluationModel
    //    {
    //        public StartUp StartUp { get; set; }
    //        public Evaluation Evaluation { get; set; }
    //    }
}
