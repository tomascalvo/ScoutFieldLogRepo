using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ScoutFieldLog_GroupProject.Models
{
    public class Evaluation
    {
        public int Id { get; set; }
        [Display(Name = "Company Id")]
        public int StartUpCompaniesId { get; set; }
        [Display(Name = "Startup")]
        public string CompanyName { get; set; }
        public string Author { get; set; }
        public DateTime Posted { get; set; }
        public string Alignments { get; set; }
        public string Themes { get; set; }
        public string Landscapes { get; set; }
        [Display(Name = "Technology Areas")]
        public string TechnologyAreas { get; set; }
        public int Uniqueness { get; set; }
        [Display(Name = "Team Strength")]
        public int Team { get; set; }
        [Display(Name = "Business Potential")]
        public int Potential { get; set; }
        [Display(Name = "Level of Interest")]
        public int Interest { get; set; }
        public string Comment { get; set; }
        public Evaluation()
        {
            Posted = DateTime.Now;
        }
    }
}
