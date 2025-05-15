using Konecta.Tools.CCaptureClient.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Konecta.Tools.CCaptureClient.Infrastructure.Services
{
    public class ApiDatabaseService : IApiDatabaseService
    {
        private readonly string _connectionString;

        public ApiDatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ApiConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoggerHelper.LogError("Database connection string is not configured in appsettings.json"); // Log error
                throw new InvalidOperationException("Database connection string is not configured in appsettings.json");
            }
            LoggerHelper.LogInfo("ApiDatabaseService initialized"); // Log service initialization
        }

        public async Task<List<string>> GetBatchClassNamesAsync()
        {
            try
            {
                LoggerHelper.LogInfo("Retrieving batch class names"); // Log query attempt
                var results = await ExecuteQueryAsync("SELECT name FROM batch_class ORDER BY name", reader => reader.GetString(0));
                LoggerHelper.LogInfo($"Retrieved {results.Count} batch class names"); // Log successful retrieval
                return results;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Failed to retrieve batch class names", ex); // Log error
                throw;
            }
        }

        public async Task<List<string>> GetFieldNamesAsync(string batchClassName)
        {
            try
            {
                LoggerHelper.LogInfo($"Retrieving field names for batch class: {batchClassName}"); // Log query attempt
                var query = @"
                    SELECT field_name 
                    FROM public.batch_field_def 
                    WHERE id_batch_class = (SELECT id_batch_class FROM public.batch_class WHERE name = @batchClassName) 
                    ORDER BY field_name ASC";

                var results = await ExecuteQueryAsync(query, reader => reader.GetString(0), ("@batchClassName", batchClassName));
                LoggerHelper.LogInfo($"Retrieved {results.Count} field names for batch class: {batchClassName}"); // Log successful retrieval
                return results;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError($"Failed to retrieve field names for batch class: {batchClassName}", ex); // Log error
                throw;
            }
        }

        public async Task<List<string>> GetPageTypesAsync(string batchClassName)
        {
            try
            {
                LoggerHelper.LogInfo($"Retrieving page types for batch class: {batchClassName}"); // Log query attempt
                var query = @"
                    SELECT pt.name 
                    FROM public.batch_class bc 
                    JOIN public.document_class dc ON dc.id_batch_class = bc.id_batch_class 
                    JOIN public.page_type pt ON pt.id_document_class = dc.id_document_class 
                    WHERE bc.name = @batchClassName 
                    ORDER BY pt.name ASC";

                var results = await ExecuteQueryAsync(query, reader => reader.GetString(0), ("@batchClassName", batchClassName));
                LoggerHelper.LogInfo($"Retrieved {results.Count} page types for batch class: {batchClassName}"); // Log successful retrieval
                return results;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError($"Failed to retrieve page types for batch class: {batchClassName}", ex); // Log error
                throw;
            }
        }

        public async Task<string> GetFieldTypeAsync(string fieldName)
        {
            try
            {
                LoggerHelper.LogInfo($"Retrieving field type for field: {fieldName}"); // Log query attempt
                var query = @"
                    SELECT ft.type_name 
                    FROM public.batch_field_def fd
                    JOIN public.field_type ft ON ft.id_field_type = fd.id_field_type
                    WHERE fd.field_name = @fieldName 
                    ORDER BY ft.type_name ASC
                    LIMIT 1";

                var results = await ExecuteQueryAsync(
                    query,
                    reader => reader.GetString(0),
                    ("@fieldName", fieldName)
                );

                var fieldType = results.FirstOrDefault() ?? string.Empty;
                LoggerHelper.LogInfo($"Retrieved field type: {fieldType} for field: {fieldName}"); // Log successful retrieval
                return fieldType;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError($"Failed to retrieve field type for field: {fieldName}", ex); // Log error
                throw;
            }
        }

        private async Task<List<T>> ExecuteQueryAsync<T>(string query, Func<NpgsqlDataReader, T> map, params (string, object)[] parameters)
        {
            var results = new List<T>();
            try
            {
                LoggerHelper.LogDebug($"Executing database query: {query}"); // Log query execution
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        foreach (var (name, value) in parameters)
                        {
                            cmd.Parameters.AddWithValue(name, value);
                        }

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.Add(map(reader));
                            }
                        }
                    }
                }
                LoggerHelper.LogDebug($"Query executed successfully, retrieved {results.Count} results"); // Log successful execution
                return results;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError($"Failed to execute database query: {query}", ex); // Log error
                throw new Exception("Database query execution failed.", ex);
            }
        }
    }
}