﻿namespace GhostQA_API.DTO_s
{
    public class Dto_IntegrationRespnse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string AppName { get; set; }
        public bool IsIntegrated { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public string Domain { get; set; }
        public string Email { get; set; }
        public string APIKey { get; set; }
    }
}
