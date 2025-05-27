using Azure;
using Konecta.Tools.CCaptureClient.Core.ApiEntities;
using Konecta.Tools.CCaptureClient.Core.Interfaces;
using Microsoft.Azure.Cosmos;
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
            LoggerHelper.LogInfo($"ApiService initialized with base URL: {baseUrl}"); // Log service initialization
        }

        public async Task<string> GetAuthTokenAsync(AuthCredentials credentials)
        {
            try
            {
                LoggerHelper.LogInfo("Requesting authentication token"); // Log token request attempt
                var content = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}/ProcessDocument/GetToken", content);

                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseContent);
                var token = result.result.ToString();
                LoggerHelper.LogInfo("Successfully retrieved authentication token"); // Log successful token retrieval
                return token;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Failed to retrieve authentication token", ex); // Log error
                throw;
            }
        }

        public async Task<string> SubmitDocumentAsync(DocumentRequest request, string authToken)
        {
            if (request == null || request.Documents == null || request.Documents.Count == 0)
            {
                LoggerHelper.LogError("Invalid document request: Request or Documents list is null or empty"); // Log error
                throw new ArgumentException("The request or its Documents list cannot be null or empty.");
            }

            if (request.BatchClassName == "Allianz Transfers")
            {
                foreach (var doc in request.Documents)
                {
                    if (string.IsNullOrEmpty(doc.PageType))
                    {
                        LoggerHelper.LogError("Invalid document: PageType is required for Allianz Transfers batch class"); // Log error
                        throw new ArgumentException("PageType is required for Allianz Transfers batch class.");
                    }
                }
            }

            HttpResponseMessage? response = null;
            try
            {
                LoggerHelper.LogInfo($"Submitting document for BatchClassName: {request.BatchClassName}, Documents: {request.Documents.Count}"); // Log submission attempt
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
                LoggerHelper.LogDebug($"Request Body: {JsonConvert.SerializeObject(requestBody, Formatting.Indented)}"); // Log request body

                // Add headers
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                _httpClient.DefaultRequestHeaders.Add("sourceSystem", request.SourceSystem);
                _httpClient.DefaultRequestHeaders.Add("channel", request.Channel);
                _httpClient.DefaultRequestHeaders.Add("interactionDateTime", request.InteractionDateTime);
                _httpClient.DefaultRequestHeaders.Add("sessionID", request.SessionID);
                _httpClient.DefaultRequestHeaders.Add("messageID", request.MessageID);
                _httpClient.DefaultRequestHeaders.Add("userCode", request.UserCode);

                // Perform the HTTP POST request
                response = await _httpClient.PostAsync($"{_baseUrl}/ProcessDocument/StartDocumentVerification", content);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the response content
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<JObject>(responseContent);
                string requestGuid = result["RequestGuid"]?.ToString();
                LoggerHelper.LogInfo($"Document submitted successfully, Request GUID: {requestGuid}"); // Log successful submission
                return requestGuid;
            }
            catch (Exception ex)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var newEx = new Exception(responseContent);
                LoggerHelper.LogError($"Failed to submit document for BatchClassName: {request.BatchClassName}", ex); // Log error
                throw newEx;
            }
        }

        public async Task<string> CheckVerificationStatusAsync(VerificationStatusRequest request, string authToken)
        {
            HttpResponseMessage? response = null;
            try
            {
                LoggerHelper.LogInfo($"Checking verification status for Request GUID: {request.RequestGuid}"); // Log status check attempt
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                _httpClient.DefaultRequestHeaders.Add("sourceSystem", request.SourceSystem);
                _httpClient.DefaultRequestHeaders.Add("channel", request.Channel);
                _httpClient.DefaultRequestHeaders.Add("interactionDateTime", request.InteractionDateTime);
                _httpClient.DefaultRequestHeaders.Add("sessionID", request.SessionID);
                _httpClient.DefaultRequestHeaders.Add("messageID", request.MessageID);
                _httpClient.DefaultRequestHeaders.Add("userCode", request.UserCode);

                response = await _httpClient.GetAsync($"{_baseUrl}/ProcessDocument/ReadDocumentVerification?requestGuid={request.RequestGuid}");

                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                LoggerHelper.LogInfo($"Verification status checked successfully for Request GUID: {request.RequestGuid}"); // Log successful status check
                return responseContent;
            }
            catch (Exception ex)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var newEx = new Exception(responseContent);
                LoggerHelper.LogError($"Failed to check verification status for Request GUID: {request.RequestGuid}", newEx); // Log error
                throw newEx;
            }
        }
    }
}