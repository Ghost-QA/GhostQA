using System.ComponentModel.DataAnnotations;

namespace GhostQA_API.Models
{
    public class ExistingSuiteRun
    {
        [Key]
        public int Id { get; set; }

        public bool IsExistingSuiteRunning { get; set; }
    }
}