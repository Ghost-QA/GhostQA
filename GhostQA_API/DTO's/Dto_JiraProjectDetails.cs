namespace GhostQA_API.DTO_s
{
    public class Jira_ProjectDetails
    {
        public List<Jira_Project> jira_projectsDetails { get; set; }
    }

    public class Jira_Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }

        public List<Jira_TestCase> TestCases { get; set; }
    }

    public class Jira_TestCase
    {
        public string Name { get; set; }
        public string Label { get; set; }
    }
}
