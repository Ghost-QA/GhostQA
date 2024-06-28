using GhostQA_API.Interfaces;

namespace GhostQA_API.DTO_s
{
    public class Dto_Load : BaseModel
    {
        public int PerformancefileId { get; set; }
        public int TotalUsers { get; set; }
        public int DurationInMinutes { get; set; }
        public int RampupTime { get; set; }
        public int RampupSteps { get; set; }
    }
}