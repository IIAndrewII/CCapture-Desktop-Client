namespace Konecta.Tools.CCaptureClient.Core.Interfaces
{
    public interface IApiDatabaseService
    {
        Task<List<string>> GetBatchClassNamesAsync();
        Task<List<string>> GetFieldNamesAsync(string batchClassName);
        Task<string> GetFieldTypeAsync(string fieldName);
        Task<List<string>> GetPageTypesAsync(string batchClassName);
    }
}
