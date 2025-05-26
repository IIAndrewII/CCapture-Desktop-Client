using Konecta.Tools.CCaptureClient.Core.DbEntities;
using Konecta.Tools.CCaptureClient.Core.Models;

namespace Konecta.Tools.CCaptureClient.Core.Interfaces
{
    public interface IDatabaseService
    {
        Task<int> SaveGroupAsync(string groupName, bool isSubmitted);
        Task<int> SaveSubmissionAsync(int groupId, string batchClassName, string sourceSystem, string channel, string sessionId, string messageId, string userCode, string interactionDateTime, string requestGuid, string authToken, string apiUrl);
        Task SaveDocumentAsync(int submissionId, string filePath, string pageType, string fileName);
        Task SaveFieldAsync(int submissionId, string fieldName, string fieldValue, string fieldType);
        Task<SubmissionDetailsModel> GetSubmissionDetailsAsync(string requestGuid);
        Task<int> SaveVerificationResponseAsync(VerificationResponse verificationResponse);
        Task<DateTime?> GetSubmissionDateAsync(string requestGuid);
        Task<bool> UpdateCheckedGuidAsync(string requestGuid);
        Task<List<object>> GetUncheckedRequestGuidsAsync();
        Task<List<VerificationResponse>> GetAllVerificationResponses();
        Task<List<VerificationResponseModel>> GetFilteredVerificationResponses(
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? status = null,
            string? sourceSystem = null,
            string? channel = null,
            string? requestGuid = null,
            string? batchClassName = null,
            string? sessionId = null,
            string? messageId = null,
            string? userCode = null);

        Task<List<string>> GetBatchClassNamesAsync();
        Task<List<string>> GetFieldNamesAsync(string batchClassName);
        Task<string> GetFieldTypeAsync(string fieldName);
        Task<List<string>> GetPageTypesAsync(string batchClassName);
    }
}
