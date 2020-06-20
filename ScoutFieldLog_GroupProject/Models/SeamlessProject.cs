using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScoutFieldLog_GroupProject.Models
{
    // PROJECTS ============================================
    #region SEAMLESS_PROJECTS
    public class SeamlessProjectList
    {
        public SeamlessProject[] records { get; set; }
    }
    public class SeamlessProject
    {
        public string id { get; set; }
        public SeamlessProjectFields fields { get; set; }
        public DateTime createdTime { get; set; }
    }
    public class SeamlessProjectFields
    {
        [JsonPropertyName("Project Name")]
        public string ProjectName { get; set; }
        [JsonPropertyName("Startup Engaged")]
        public string StartupEngaged { get; set; }
        [JsonPropertyName("Partners Involved")]
        public string PartnersInvolved { get; set; }
        [JsonPropertyName("Project Summary")]
        public string ProjectSummary { get; set; }
        [JsonPropertyName("Engagement Phase")]
        public string EngagementPhase { get; set; }
        [JsonPropertyName("Engagement Progress")]
        public string EngagementProgress { get; set; }
        [JsonPropertyName("Engagement Type")]
        public string EngagementType { get; set; }
        [JsonPropertyName("First Engagement End Date")]
        public string FirstEngagementEndDate { get; set; }
        [JsonPropertyName("Post-Engagement Maturity Score")]
        public string PostEngagementMaturityScore { get; set; }
        [JsonPropertyName("One Year Post Engagement Date")]
        public string OneYearPostEngagementDate { get; set; }
        [JsonPropertyName("One Year Post Engagement Maturity Score")]
        public string OneYearPostEngagementMaturityScore { get; set; }
        [JsonPropertyName("Parnters Interested in Secondary Engagement")]
        public string ParntersInterestedinSecondaryEngagement { get; set; }
        [JsonPropertyName("Type of Engagement")]
        public string TypeofEngagement { get; set; }
        [JsonPropertyName("Ongoing Status")]
        public string OngoingStatus { get; set; }
        [JsonPropertyName("Project Lead")]
        public string ProjectLead { get; set; }
        [JsonPropertyName("Kickoff Date")]
        public string KickoffDate { get; set; }
        public string StatusSchedule { get; set; }
        [JsonPropertyName("Post-Engagement Seamless Investment")]
        public string PostEngagementSeamlessInvestment { get; set; }
        [JsonPropertyName("Second Engagement Start Date")]
        public string SecondEngagementStartDate { get; set; }
    }
    #endregion
}
