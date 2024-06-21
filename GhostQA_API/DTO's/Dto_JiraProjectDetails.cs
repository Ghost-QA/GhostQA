using Newtonsoft.Json;

namespace GhostQA_API.DTO_s
{
    public class Jira_ProjectDetails
    {
        public List<Jira_Project> jira_projectsDetails { get; set; } = new List<Jira_Project>();

        public ProjectSummaryCoverageDetails Summary { get; set; } = new ProjectSummaryCoverageDetails();
    }

    public class Jira_Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public List<Jira_TestCase> TestCases { get; set; } = new List<Jira_TestCase>();
        public int totalTestcases { get; set; }
        public double perAutomatedTestcases { get; set; }
        public double perNotAutomatedTestcases { get; set; }
    }

    public class ProjectSummaryCoverageDetails
    {
        public List<string> Projects { get; set; } = new List<string>();
        public List<Jira_TestCase> TestCases { get; set; } = new List<Jira_TestCase>();
        public int TotalProject { get; set; }
        public int TotalTestCases { get; set; }
        public double perAutomatedTestcases { get; set; }
        public double perNotAutomatedTestcases { get; set; }
    }

    public class Jira_TestCase
    {
        public string Name { get; set; }
        public string Label { get; set; }
    }
    public class Root
    {
        //[JsonProperty("next")]
        //public object Next { get; set; }

        //[JsonProperty("startAt")]
        //public int StartAt { get; set; }

        //[JsonProperty("maxResults")]
        //public int MaxResults { get; set; }

        //[JsonProperty("total")]
        //public int Total { get; set; }

        //[JsonProperty("isLast")]
        //public bool IsLast { get; set; }

        [JsonProperty("values")]
        public List<Value> Values { get; set; }
    }

    public class Value
    {
        //[JsonProperty("id")]
        //public long Id { get; set; }

        //[JsonProperty("key")]
        //public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("labels")]
        public List<string> Labels { get; set; }

    }
}
