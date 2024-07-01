using GhostQA_API.Interfaces;

namespace GhostQA_API.DTO_s
{
    public class Dto_Execution : BaseModel
    {
        public string testSuiteName { get; set; }
        public string userId { get; set; }
        public int rootId { get; set; }
        public string userEmail { get; set; }
    }
}