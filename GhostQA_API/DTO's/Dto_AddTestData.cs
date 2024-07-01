using GhostQA_API.Interfaces;

namespace GhostQA_API.DTO_s
{
    public class Dto_AddTestData : BaseModel
    {
        public int Id { get; set; }
        public int PerformanceFileId { get; set; }
        public IFormFile File { get; set; }
    }
}