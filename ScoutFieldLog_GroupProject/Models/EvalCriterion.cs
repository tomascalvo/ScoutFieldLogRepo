using System;
using System.Collections.Generic;

namespace ScoutFieldLog_GroupProject.Models
{
    public partial class EvalCriterion
    {
        public int EvalCriterionId { get; set; }
        public string EvalCriterionName { get; set; }
        public int? EvalCategoryId { get; set; }

        public virtual EvalCategory EvalCategory { get; set; }
    }
}
