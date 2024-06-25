using GhostQA_API.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GhostQA_API.Models
{
    public class TestSuites : BaseModel
    {
        [Key]
        public int TestSuiteId { get; set; }

        [Required(ErrorMessage = "Test Suite Name is required."), Column("TestSuiteName", TypeName = "varchar(100)")]
        public string TestSuiteName { get; set; }

        public string? TestSuiteType { get; set; } // "BuiltIn" or "Custom"

        public bool SendEmail { get; set; }

        [Required(ErrorMessage = "Environment is required.")]
        public int EnvironmentId { get; set; }

        public string? SelectedTestCases { get; set; }

        [NotMapped]
        public List<SelectListItem> AllTestCases { get; set; }

        public string? Description { get; set; }
        public int? TestUserId { get; set; }
        public int RootId { get; set; }
    }
}