using System;
using System.Collections.Generic;

namespace ScoutFieldLog_GroupProject.Models
{
    public partial class EvalCategory
    {
        public EvalCategory()
        {
            EvalCriterion = new HashSet<EvalCriterion>();
        }

        public int EvalCategoryId { get; set; }
        public string EvalCategoryName { get; set; }

        public virtual ICollection<EvalCriterion> EvalCriterion { get; set; }
    }
}
