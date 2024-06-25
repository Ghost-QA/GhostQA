using GhostQA_API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GhostQA_API.Models
{
    public class Load : BaseModel
    {
        [Key]
        public int Id { get; set; }

        public int PerformanceFileId { get; set; }
        public int? TotalUsers { get; set; }
        public int? DurationInMinutes { get; set; }
        public int? RampUpTimeInSeconds { get; set; }
        public int? RampUpSteps { get; set; }
    }
}