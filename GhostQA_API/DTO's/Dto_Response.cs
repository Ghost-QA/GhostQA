using Newtonsoft.Json;

namespace GhostQA_API.DTO_s
{
    public class Dto_Response
    {
        [JsonProperty("status")]
        public string? status { get; set; }

        [JsonProperty("message")]
        public string? message { get; set; }

        public object? Data { get; set; }
    }
}