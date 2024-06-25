using GhostQA_API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GhostQA_API.Models
{
    public class FuncationalTest : BaseModel
    {
        [Key]
        public int RootId { get; set; }

        public int? Node { get; set; }
        public int? Parent { get; set; }
        public string Name { get; set; }
    }
}