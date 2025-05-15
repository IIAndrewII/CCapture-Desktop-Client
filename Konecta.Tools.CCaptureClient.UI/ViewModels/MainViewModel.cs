using Konecta.Tools.CCaptureClient.Core.ApiEntities;
using Konecta.Tools.CCaptureClient.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Konecta.Tools.CCaptureClient.Core.DbEntities;

namespace Konecta.Tools.CCaptureClient.UI.ViewModels
{
    public class MainViewModel
    {
        private IApiService _apiService;
        private readonly IFileService _fileService;
        private readonly IApiDatabaseService _apiDatabaseService;
        private readonly IDatabaseService _databaseService;
        private readonly IConfiguration _configuration;
        private string _authToken;
        private string _lastRequestGuid;

        public MainViewModel(
            IApiService apiService,
            IFileService fileService,
            IApiDatabaseService apiDatabaseService,
            IDatabaseService databaseService,
            IConfiguration configuration)
        {
            _apiService = apiService;
            _fileService = fileService;
            _apiDatabaseService = apiDatabaseService;
            _databaseService = databaseService;
            _configuration = configuration;
        }

        public void UpdateApiService(IApiService apiService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        }

        public async Task<string> GetAuthTokenAsync(string appName, string appLogin, string appPassword)
        {
            var credentials = new AuthCredentials
            {
                ApplicationName = appName,
                AppLogin = appLogin,
                AppPassword = appPassword
            };

            _authToken = await _apiService.GetAuthTokenAsync(credentials);
            return _authToken;
        }

        public async Task<string> SubmitDocumentAsync(
            string batchClassName,
            string sourceSystem,
            string channel,
            string sessionId,
            string messageId,
            string userCode,
            string interactionDateTime,
            List<Core.ApiEntities.Field> fields,
            string groupName,
            List<Document_Row> documents)
        {
            try
            {
                // Generate a temporary RequestGuid
                string tempRequestGuid = Guid.NewGuid().ToString();

                // Save group
                var groupId = await _databaseService.SaveGroupAsync(groupName, true);

                // Save submission
                var submissionId = await _databaseService.SaveSubmissionAsync(
                    groupId,
                    batchClassName,
                    sourceSystem,
                    channel,
                    sessionId,
                    messageId,
                    userCode,
                    interactionDateTime,
                    tempRequestGuid,
                    _authToken);

                // Save fields
                foreach (var field in fields)
                {
                    var fieldType = await _apiDatabaseService.GetFieldTypeAsync(field.FieldName);
                    await _databaseService.SaveFieldAsync(submissionId, field.FieldName, field.FieldValue, fieldType);
                }

                // Save documents
                foreach (var doc in documents)
                {
                    var fileName = _fileService.GetFileName(doc.FilePath);
                    await _databaseService.SaveDocumentAsync(submissionId, doc.FilePath, doc.PageType, fileName);
                }

                // Prepare and submit to API
                var documentList = documents.Select(doc => new Core.ApiEntities.Document
                {
                    FileName = _fileService.GetFileName(doc.FilePath),
                    Buffer = _fileService.ReadFileAsBase64(doc.FilePath),
                    PageType = doc.PageType
                }).ToList();

                var request = new DocumentRequest
                {
                    BatchClassName = batchClassName,
                    Documents = documentList,
                    SourceSystem = sourceSystem,
                    Channel = channel,
                    InteractionDateTime = interactionDateTime,
                    SessionID = sessionId,
                    MessageID = messageId,
                    UserCode = userCode,
                    Fields = fields
                };

                _lastRequestGuid = await _apiService.SubmitDocumentAsync(request, _authToken);

                // Update submission with the API-provided RequestGuid
                using (var context = new CCaptureDbContext(new DbContextOptionsBuilder<CCaptureDbContext>()
                    .UseSqlServer(_configuration.GetConnectionString("DefaultConnection")).Options))
                {
                    var submission = context.Submissions.FirstOrDefault(s => s.RequestGuid == tempRequestGuid);
                    if (submission != null)
                    {
                        submission.RequestGuid = _lastRequestGuid;
                        await context.SaveChangesAsync();
                    }
                }

                return _lastRequestGuid;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to submit document: {ex.Message}", ex);
            }
        }

        public async Task<string> CheckVerificationStatusAsync(
            string requestGuid,
            string sourceSystem,
            string channel,
            string sessionId,
            string messageId,
            string userCode,
            string interactionDateTime)
        {
            var request = new VerificationStatusRequest
            {
                RequestGuid = string.IsNullOrEmpty(requestGuid) ? _lastRequestGuid : requestGuid,
                SourceSystem = sourceSystem,
                Channel = channel,
                InteractionDateTime = interactionDateTime,
                SessionID = sessionId,
                MessageID = messageId,
                UserCode = userCode
            };

            return await _apiService.CheckVerificationStatusAsync(request, _authToken);
        }
    }
}