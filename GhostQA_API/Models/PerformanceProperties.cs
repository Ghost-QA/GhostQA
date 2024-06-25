using GhostQA_API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GhostQA_API.Models
{
    public class PerformanceProperties : BaseModel
    {
        [Key]
        public int Id { get; set; }

        public int PerformanceFileId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}