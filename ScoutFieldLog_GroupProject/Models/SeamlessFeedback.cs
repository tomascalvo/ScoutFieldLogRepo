using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutFieldLog_GroupProject.Models
{
    // FEEDBACK ============================================
    #region SEAMLESS_FEEDBACK
    public class SeamlessFeedbackList
    {
        public SeamlessFeedback[] records { get; set; }
        public string offset { get; set; }
    }

    public class SeamlessFeedback
    {
        public string id { get; set; }
        public FeedbackFields fields { get; set; }
        public DateTime createdTime { get; set; }
    }

    public class FeedbackFields
    {
        public string IntroDate { get; set; }
        public string Startup { get; set; }
        public string YourOrganization { get; set; }
        public string Alignmenttoyourorganization { get; set; }
        public string UniquenessofTech { get; set; }
        public string TeamStrength { get; set; }
        public string BusinessPotential { get; set; }
        public string LevelofInterestinProject { get; set; }
        public string Questions { get; set; }
    }
    #endregion

}
