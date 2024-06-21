using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace GhostQA_API.Services
{
    public class ZephyrService
    {
        private readonly string _baseUri = "https://api.zephyrscale.smartbear.com/v2"; // Base URL for Zephyr Scale API
        private readonly string _username = "mukeshstest@gmail.com";
        private readonly string _apiToken = @"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJjb250ZXh0Ijp7ImJhc2VVcmwiOiJodHRwczovL211a2VzaHN0ZXN0LmF0bGFzc2lhbi5uZXQiLCJ1c2VyIjp7ImFjY291bnRJZCI6IjcxMjAyMDpmZWQ0MGEwMC1jZTMzLTRmODUtODM5MS0xMzdlNjIyNTYzYTQifX0sImlzcyI6ImNvbS5rYW5vYWgudGVzdC1tYW5hZ2VyIiwic3ViIjoiYWM4NDczYWUtMTAxZS0zYWEzLWEyYTAtMmVkNjlmMTQ3OWU4IiwiZXhwIjoxNzUwNDUzOTM3LCJpYXQiOjE3MTg5MTc5Mzd9.e6DssF71hNglopq5S6IDv1yHQjj0Ef4o6HbcMlRzdXM";
        public ZephyrService()
        {
        }

        public async Task<string> GetTestCasesAsync(string projectKey)
        {
            HttpClient _httpClient = new HttpClient();
            var authToken = Encoding.ASCII.GetBytes($"{_username}:{_apiToken}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

            var endpoint = $"{_baseUri}/testcases?projectKey={projectKey}";
            using (var response = await _httpClient.GetAsync(endpoint))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

    }
}
