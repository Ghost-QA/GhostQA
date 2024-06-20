﻿namespace GhostQA_API.DTO_s
{
    public class Dto_TestCaseData
    {
        public int TestCaseId { get; set; }
        public string TestSuiteName { get; set; }
        public string TestRunName { get; set; }
        public string TestCaseName { get; set; }
        public string TestCaseStatus { get; set; }
        public string TestCaseVideoURL { get; set; }
        public string TestSuiteStartDateTime { get; set; }
        public string TestSuiteEndDateTime { get; set; }
        public string TestRunStartDateTime { get; set; }
        public string TestRunEndDateTime { get; set; }
        public string TestCaseSteps { get; set; }
        public string TesterName { get; set; }
        public string TestEnvironment { get; set; }
        public string TestBrowserName { get; set; }
        public int RootId { get; set; }
        public int ApplicationId { get; set; }
        public Guid TenantId { get; set; }
        public Guid OrganizationId { get; set; }
    }
}