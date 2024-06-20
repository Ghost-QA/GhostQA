using Newtonsoft.Json;
using GhostQA_API.DTO_s;
using System.Text;

namespace GhostQA_API.Services
{
    public class SuiteService
    {
        public async Task RunSuite(Dto_SuiteRun model)
        {
            Dto_Execution data = new Dto_Execution()
            {
                testSuiteName = model.SuiteName,
                userId = model.UserId
            };

            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("X-Api-TimeZone", model.Header);
            httpClient.DefaultRequestHeaders.Add("Authorization", model.Token);

            HttpResponseMessage response = await httpClient.PostAsync(model.BaseUrl, content);

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
