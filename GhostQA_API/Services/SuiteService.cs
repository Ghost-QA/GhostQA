using Newtonsoft.Json;
using GhostQA_API.DTO_s;
using System.Text;

namespace GhostQA_API.Services
{
    public class SuiteService
    {
        private readonly HttpClient _httpClient;

        public SuiteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task RunSuite(Dto_SuiteRun model)
        {
            Dto_Execution data = new Dto_Execution()
            {
                testSuiteName = model.SuiteName,
                userId = model.UserId
            };

            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Add("X-Api-TimeZone", model.Header);
            _httpClient.DefaultRequestHeaders.Add("Authorization", model.Token);

            HttpResponseMessage response = await _httpClient.PostAsync(model.BaseUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
            }
            else
            {
            }
        }
    }
}
