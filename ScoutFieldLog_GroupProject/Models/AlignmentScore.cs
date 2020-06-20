using System;
using System.Collections.Generic;

namespace ScoutFieldLog_GroupProject.Models
{
    public partial class AlignmentScore
    {
        public int Id { get; set; }
        public int EvaluationId { get; set; }
        public string PartnerCompanyId { get; set; }
        public int Score { get; set; }

        public virtual Evaluation Evaluation { get; set; }
    }
}
