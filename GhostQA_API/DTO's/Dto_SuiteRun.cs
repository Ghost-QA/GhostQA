﻿namespace GhostQA_API.DTO_s
{
    public class Dto_SuiteRun
    {
        public string BaseUrl { get; set; }
        public string SuiteName { get; set; }
        public string Header { get; set; }
        public string UserId { get; set; }
        public DateTime EndDate { get; set; }
        public int RootId { get; set; }
    }
}
