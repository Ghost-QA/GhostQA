using GhostQA_API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GhostQA_API.Models
{
    public class FunctionalSuiteRelation : BaseModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int? Parent { get; set; }
    }
}