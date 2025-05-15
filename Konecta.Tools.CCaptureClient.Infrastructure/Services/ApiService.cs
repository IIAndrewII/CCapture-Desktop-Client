// CCaptureMockApiClient.Infrastructure/Services/ApiService.cs
using Konecta.Tools.CCaptureClient.Core.ApiEntities;
using Konecta.Tools.CCaptureClient.Core.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Konecta.Tools.CCaptureClient.Infrastructure.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<string> GetAuthTokenAsync(AuthCredentials credentials)
        {
            var content = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/ProcessDocument/GetToken", content);

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseContent);
            return result.result;
        }

        public async Task<string> SubmitDocumentAsync(DocumentRequest request, string authToken)
        {
            if (request == null || request.Documents == null || request.Documents.Count == 0)
            {
                throw new ArgumentException("The request or its Documents list cannot be null or empty.");
            }

            if (request.BatchClassName == "Allianz Transfers")
            {
                foreach (var doc in request.Documents)
                {
                    if (string.IsNullOrEmpty(doc.PageType))
                    {
                        throw new ArgumentException("PageType is required for Allianz Transfers batch class.");
                    }
                }
            }

            var requestBody = new
            {
                request.BatchClassName,
                request.Fields,
                request.Documents
            };

            // Serialize the body to JSON
            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            // Log headers and body
            Debug.WriteLine("Request Headers:");
            foreach (var header in _httpClient.DefaultRequestHeaders)
            {
                Debug.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }

            Debug.WriteLine("Request Body:");
            Debug.WriteLine(JsonConvert.SerializeObject(requestBody, Formatting.Indented));

            // Add headers
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            _httpClient.DefaultRequestHeaders.Add("sourceSystem", request.SourceSystem);
            _httpClient.DefaultRequestHeaders.Add("channel", request.Channel);
            _httpClient.DefaultRequestHeaders.Add("interactionDate-Time", request.InteractionDateTime);
            _httpClient.DefaultRequestHeaders.Add("sessionID", request.SessionID);
            _httpClient.DefaultRequestHeaders.Add("messageID", request.MessageID);
            _httpClient.DefaultRequestHeaders.Add("userCode", request.UserCode);

            // Perform the HTTP POST request
            var response = await _httpClient.PostAsync($"{_baseUrl}/ProcessDocument/StartDocumentVerification", content);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Read the response content
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<JObject>(responseContent);
            string requestGuid = result["requestGuid"]?.ToString();
            return requestGuid;
        }


        public async Task<string> CheckVerificationStatusAsync(VerificationStatusRequest request, string authToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            _httpClient.DefaultRequestHeaders.Add("sourceSystem", request.SourceSystem);
            _httpClient.DefaultRequestHeaders.Add("channel", request.Channel);
            _httpClient.DefaultRequestHeaders.Add("interactionDate-Time", request.InteractionDateTime);
            _httpClient.DefaultRequestHeaders.Add("sessionID", request.SessionID);
            _httpClient.DefaultRequestHeaders.Add("messageID", request.MessageID);
            _httpClient.DefaultRequestHeaders.Add("userCode", request.UserCode);

            var response = await _httpClient.GetAsync($"{_baseUrl}/ProcessDocument/ReadDocumentVerification?requestGuid={request.RequestGuid}");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}