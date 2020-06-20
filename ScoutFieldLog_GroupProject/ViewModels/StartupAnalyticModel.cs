using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using ScoutFieldLog_GroupProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ScoutFieldLog_GroupProject.ViewModels
{
    public class StartupAnalyticModel
    {
        public StartUpCompanies StartUpCo { get; set; }
        public List<AlignmentAM> Alignments { get; set; }
        public List<ThemeAM> Themes { get; set; }
        public List<LandscapeAM> Landscapes { get; set; }
        [DisplayName("Technology Areas")]
        public List<TechnologyAreaAM> TechnologyAreas { get; set; }
        public UniquenessAM Uniqueness { get; set; }
        [DisplayName("Team Strength")]
        public TeamStrengthAM TeamStrength { get; set; }
        public BusinessPotentialAM BusinessPotential { get; set; }
        [DisplayName("Level of Interest")]
        public InterestAM Interest { get; set; }
        public List<Comment> Comments { get; set; }
        public List<UserAM> Users { get; set; }
        public StartupAnalyticModel(StartUpCompanies startup, List<Evaluation> evaluations, List<EvalCriterion> ecriteria, List<AlignmentScore> alignmentScores)
        {
            StartUpCo = startup;
            Alignments = new List<AlignmentAM>();
            
            var partnerCriteria = ecriteria.Where(x => x.EvalCategoryId.Equals(1));
            List<string> partnerNames = new List<string>();
            foreach(EvalCriterion ec in partnerCriteria)
            {
                Alignments.Add(new AlignmentAM(ec, alignmentScores));
            }
            Themes = new List<ThemeAM>();
            var themesCriteria = ecriteria.Where(x => x.EvalCategoryId.Equals(2));
            foreach(EvalCriterion ec in themesCriteria)
            {
                Themes.Add(new ThemeAM(ec, evaluations));
            }
            Landscapes = new List<LandscapeAM>();
            var landscapesCriteria = ecriteria.Where(x => x.EvalCategoryId.Equals(3));
            foreach (EvalCriterion ec in landscapesCriteria)
            {
                Landscapes.Add(new LandscapeAM(ec, evaluations));
            }
            TechnologyAreas = new List<TechnologyAreaAM>();
            var techAreasCriteria = ecriteria.Where(x => x.EvalCategoryId.Equals(4));
            foreach (EvalCriterion ec in techAreasCriteria)
            {
                TechnologyAreas.Add(new TechnologyAreaAM(ec, evaluations));
            }
            Uniqueness = new UniquenessAM(evaluations);
            TeamStrength = new TeamStrengthAM(evaluations);
            BusinessPotential = new BusinessPotentialAM(evaluations);
            Interest = new InterestAM(evaluations);
            Comments = new List<Comment>();
            foreach (Evaluation evaluation in evaluations)
            {
                Comments.Add(new Comment(evaluation));
            }
        }
    }
    public class CriterionAM
    {
        public string Name { get; set; }
        public double ScoreAvgConnectors { get; set; }

    }
    public class CriterionBool : CriterionAM
    {
        public bool ScoreUserA { get; set; }
        public bool ScoreUserB { get; set; }

    }
    public class CriterionInt
    {
        public int ScoreUserA { get; set; }
        public int ScoreUserB { get; set; }
        public double ScoreAvgConnectors { get; set; }
        public double ScoreAvgPartners { get; set; }
    }
    public class AlignmentAM : CriterionAM
    {
        public int PartnerId { get; set; }
        public int ScoreUserA { get; set; }
        public int ScoreUserB { get; set; }
        public AlignmentAM(EvalCriterion eCriterion, List<AlignmentScore> alignmentScores)
        {
            Name = eCriterion.EvalCriterionName;
            PartnerId = eCriterion.EvalCriterionId;
            var hits = alignmentScores.Where(s => s.PartnerCompanyId.Equals(eCriterion.EvalCriterionId));
            int sum = 0;
            foreach (AlignmentScore s in hits)
            {
                sum += s.Score;
            }
            if (hits.Count() == 0)
            {
                ScoreAvgConnectors = 0;
            }
            else
            {
                ScoreAvgConnectors = sum / hits.Count();
            }
        }
    }
    public class ThemeAM : CriterionBool
    {
        public ThemeAM(EvalCriterion eCriterion, List<Evaluation> evaluations, UserAM userA, UserAM userB)
        {
            Name = eCriterion.EvalCriterionName;
            var hits = evaluations.Where(e => e.Themes.Contains(eCriterion.EvalCriterionName.ToString().Trim()));
            ScoreAvgConnectors = hits.Count()/evaluations.Count();
            ScoreUserA = hits.Where(e => e.AuthorId.Equals(userA.UserId)).Any();
            ScoreUserB = hits.Where(e => e.AuthorId.Equals(userB.UserId)).Any();
        }
        public ThemeAM(EvalCriterion eCriterion, List<Evaluation> evaluations)
        {
            Name = eCriterion.EvalCriterionName;
            var hits = evaluations.Where(e => e.Themes.ToLower().Trim().Contains(eCriterion.EvalCriterionName.ToString().ToLower().Trim()));
            ScoreAvgConnectors = hits.Count() / evaluations.Count();
        }
    }
    public class LandscapeAM : CriterionBool
    {
        public LandscapeAM(EvalCriterion eCriterion, List<Evaluation> evaluations)
        {
            Name = eCriterion.EvalCriterionName;
            var hits = evaluations.Where(e => e.Themes.Contains(eCriterion.EvalCriterionName.ToString().Trim()));
            ScoreAvgConnectors = hits.Count() / evaluations.Count();
          
        }
    }
    public class TechnologyAreaAM : CriterionBool
    {
        public TechnologyAreaAM(EvalCriterion eCriterion, List<Evaluation> evaluations)
        {
            Name = eCriterion.EvalCriterionName;
            var hits = evaluations.Where(e => e.Themes.Contains(eCriterion.EvalCriterionName.ToString().Trim()));
            ScoreAvgConnectors = hits.Count() / evaluations.Count();
        }
    }
    public class UniquenessAM : CriterionInt
    {
        public UniquenessAM(List<Evaluation> evaluations)
        {
            int sum = 0;
            foreach (Evaluation e in evaluations)
            {
                sum += e.UniquenessScore;
            }
            ScoreAvgConnectors = sum / evaluations.Count();
        }
    }
    public class TeamStrengthAM : CriterionInt
    {
        public TeamStrengthAM(List<Evaluation> evaluations)
        {
            int sum = 0;
            foreach (Evaluation e in evaluations)
            {
                sum += e.TeamScore;
            }
            ScoreAvgConnectors = sum / evaluations.Count();
        }
    }
    public class BusinessPotentialAM : CriterionInt
    {
        public BusinessPotentialAM (List<Evaluation> evaluations)
        {
            int sum = 0;
            foreach (Evaluation e in evaluations)
            {
                sum += e.PotentialScore;
            }
            ScoreAvgConnectors = sum / evaluations.Count();
        }
    }
    public class InterestAM : CriterionInt
    {
        public InterestAM (List<Evaluation> evaluations)
        {
            int sum = 0;
            foreach (Evaluation e in evaluations)
            {
                sum += e.InterestScore;
            }
            ScoreAvgConnectors = sum / evaluations.Count();
        }
    }
    public class Comment
    {
        public int UserId { get; set; }
        public string Body { get; set; }
        public Comment (Evaluation evaluation)
        {
            UserId = evaluation.AuthorId;
            Body = evaluation.Comment;
        }
    }
    public class UserAM
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
