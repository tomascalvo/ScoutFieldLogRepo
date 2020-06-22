using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ScoutFieldLog_GroupProject.Models
{
    public class Analysis
    {
        [Display(Name = "Company Id")]
        public int StartUpCompaniesId { get; set; }
        public string CompanyName { get; set; }
        public List<AlignmentQuotient> AlignmentQuotients { get; set; }
        public List<ThemeQuotient> ThemeQuotients { get; set; }
        public List<LandscapeQuotient> LandscapeQuotients { get; set; }
        public List<TechnologyAreaQuotient> TechnologyAreaQuotients { get; set; }
        [Display(Name = "Uniqueness Quotient")]
        public double UniquenessQuotient { get; set; }
        [Display(Name = "Team Strength Quotient")]
        public double TeamStrengthQuotient { get; set; }
        [Display(Name = "Business Potential Quotient")]
        public double BusinessPotentialQuotient { get; set; }
        [Display(Name = "Level of Interest Quotient")]
        public double InterestQuotient { get; set; }
        public List<Comment> Comments { get; set; }
        public Analysis(List<Evaluation> evaluations, List<PartnerCompany> partners, List<Theme> themes, List<Landscape> landscapes, List<TechnologyArea> technologyAreas)
        {
            double evaluationCount = evaluations.Count();
            AlignmentQuotients = new List<AlignmentQuotient>();
            foreach (PartnerCompany partner in partners)
            {
                var alignments = evaluations.Select(e => e.Alignments);
                double hits = alignments.Where(a => a.Contains(partner.CompanyName.ToString())).Count();
                double quotient = hits / evaluationCount;
                AlignmentQuotients.Add(new AlignmentQuotient(partner.CompanyName.ToString(), quotient));
            }
            ThemeQuotients = new List<ThemeQuotient>();
            foreach (Theme theme in themes)
            {
                var tEvals = evaluations.Select(e => e.Themes);
                double hits = tEvals.Where(t => t.Contains(theme.Name.ToString())).Count();
                double quotient = hits / evaluationCount;
                ThemeQuotients.Add(new ThemeQuotient(theme.Name.ToString(), quotient));
            }
            LandscapeQuotients = new List<LandscapeQuotient>();
            foreach (Landscape landscape in landscapes)
            {
                var lEvals = evaluations.Select(e => e.Landscapes);
                double hits = lEvals.Where(l => l.Contains(landscape.Name.ToString())).Count();
                double quotient = hits / evaluationCount;
                LandscapeQuotients.Add(new LandscapeQuotient(landscape.Name.ToString(), quotient));
            }
            TechnologyAreaQuotients = new List<TechnologyAreaQuotient>();
            foreach (TechnologyArea technologyArea in technologyAreas)
            {
                var tEvals = evaluations.Select(e => e.TechnologyAreas);
                double hits = tEvals.Where(t => t.Contains(technologyArea.Name.ToString())).Count();
                double quotient = hits / evaluationCount;
                TechnologyAreaQuotients.Add(new TechnologyAreaQuotient(technologyArea.Name.ToString(), quotient));
            }
            Comments = new List<Comment>();
            foreach (Evaluation evaluation in evaluations)
            {
                string authorName = evaluation.Author.ToString();
                string content = evaluation.Comment.ToString();
                Comments.Add(new Comment(authorName, content));
            }
            UniquenessQuotient = evaluations.Select(e => e.Uniqueness).Sum() / evaluationCount;
            TeamStrengthQuotient = evaluations.Select(e => e.Team).Sum() / evaluationCount;
            BusinessPotentialQuotient = evaluations.Select(e => e.Potential).Sum() / evaluationCount;
            InterestQuotient = evaluations.Select(e => e.Interest).Sum() / evaluationCount;
        }
    }
    public class AlignmentQuotient
    {
        public string PartnerName { get; set; }
        public double Quotient { get; set; }
        public AlignmentQuotient(string partnerName, double quotient)
        {
            PartnerName = partnerName;
            Quotient = quotient;
        }
    }
    public class ThemeQuotient
    {
        public string Name { get; set; }
        public double Quotient { get; set; }
        public ThemeQuotient(string name, double quotient)
        {
            Name = name;
            Quotient = quotient;
        }
    }
    public class LandscapeQuotient
    {
        public string Name { get; set; }
        public double Quotient { get; set; }
        public LandscapeQuotient(string name, double quotient)
        {
            Name = name;
            Quotient = quotient;
        }
    }
    public class TechnologyAreaQuotient
    {
        public string Name { get; set; }
        public double Quotient { get; set; }
        public TechnologyAreaQuotient(string name, double quotient)
        {
            Name = name;
            Quotient = quotient;
        }
    }
    public class Comment
    {
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public Comment(string authorName, string content)
        {
            AuthorName = authorName;
            Content = content;
        }
    }
}