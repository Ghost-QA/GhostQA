using GhostQA_API.DTO_s;
using System.Net.Http.Headers;

namespace GhostQA_API.Services
{
    public class ZephyrService
    {
        private readonly IConfiguration _configuration;
        public ZephyrService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Root> GetTestCasesAsync(string projectKey)
        {
            HttpClient _httpClient = new HttpClient();
            Root result = new Root();
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["Integration:ZephyrApiKey"]);

                using (var response = await _httpClient.GetAsync($"{_configuration["Integration:ZephyrBaseUri"]}/testcases?projectKey={projectKey}"))
                {
                    var obj = await response.Content.ReadAsStringAsync();
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
