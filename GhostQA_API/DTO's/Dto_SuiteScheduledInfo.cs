using GhostQA_API.Interfaces;

namespace GhostQA_API.DTO_s
{
    public class Dto_SuiteScheduledInfo : BaseModel
    {
        public int Id { get; set; }
        public string Interval { get; set; }
        public string SuiteName { get; set; }
        public int RootId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDate { get; set; }
        public List<DayOfWeek> DaysOfWeek { get; set; } = new List<DayOfWeek>();
        public int DayOfMonth { get; set; }
        public int RepeatEvery { get; set; }
        public string JobId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
    }
}
