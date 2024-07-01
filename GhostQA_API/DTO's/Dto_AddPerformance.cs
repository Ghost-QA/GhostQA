﻿using GhostQA_API.Interfaces;

namespace GhostQA_API.DTO_s
{
    public class Dto_AddPerformance : BaseModel
    {
        public int Id { get; set; }
        public int RootId { get; set; }
        public string TestCaseName { get; set; }
        public string FileName { get; set; }
        public IFormFile BinaryData { get; set; }
    }
}