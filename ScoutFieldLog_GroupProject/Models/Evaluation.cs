using System;
using System.Collections.Generic;

namespace ScoutFieldLog_GroupProject.Models
{
    public partial class Evaluation
    {
        public Evaluation()
        {
            AlignmentScore = new HashSet<AlignmentScore>();
        }

        public int Id { get; set; }
        public int StartUpCompaniesId { get; set; }
        public int AuthorId { get; set; }
        public DateTime? DateEvaluated { get; set; }
        public string Themes { get; set; }
        public string Landscapes { get; set; }
        public string TechnologyAreas { get; set; }
        public int UniquenessScore { get; set; }
        public int TeamScore { get; set; }
        public int PotentialScore { get; set; }
        public int InterestScore { get; set; }
        public string Comment { get; set; }

        public virtual StartUpCompanies StartUpCompanies { get; set; }
        public virtual ICollection<AlignmentScore> AlignmentScore { get; set; }
    }
}
