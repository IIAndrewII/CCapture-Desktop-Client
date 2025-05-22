using Konecta.Tools.CCaptureClient.Core.DbEntities;
using Konecta.Tools.CCaptureClient.Core.Interfaces;
using Konecta.Tools.CCaptureClient.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Konecta.Tools.CCaptureClient.Infrastructure.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IConfiguration _configuration;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
            LoggerHelper.LogInfo("DatabaseService initialized"); // Log service initialization
        }

        private CCaptureDbContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CCaptureDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            var context = new CCaptureDbContext(optionsBuilder.Options);
            LoggerHelper.LogDebug("Created database context"); // Log context creation
            return context;
        }

        // Get batch class names from API_batch_class
        public async Task<List<string>> GetBatchClassNamesAsync()
        {
            using (var context = CreateContext())
            {
                try
                {
                    LoggerHelper.LogInfo("Retrieving batch class names"); // Log query attempt
                    var results = await context.ApiBatchClasses
                        .Select(bc => bc.Name)
                        .OrderBy(name => name)
                        .ToListAsync();
                    LoggerHelper.LogInfo($"Retrieved {results.Count} batch class names"); // Log successful retrieval
                    return results;
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError("Failed to retrieve batch class names", ex); // Log error
                    throw;
                }
            }
        }

        // Get field names for a batch class from API_batch_field_def
        public async Task<List<string>> GetFieldNamesAsync(string batchClassName)
        {
            using (var context = CreateContext())
            {
                try
                {
                    LoggerHelper.LogInfo($"Retrieving field names for batch class: {batchClassName}");

                    var results = await context.ApiBatchFieldDefs
                        .Where(bfd => bfd.IdBatchClass == context.ApiBatchClasses
                            .Where(bc => bc.Name == batchClassName)
                            .Select(bc => bc.IdBatchClass)
                            .FirstOrDefault())
                        .Select(bfd => bfd.FieldName)
                        .OrderBy(fieldName => fieldName)
                        .ToListAsync();

                    LoggerHelper.LogInfo($"Retrieved {results.Count} field names for batch class: {batchClassName}");
                    return results;
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError($"Failed to retrieve field names for batch class: {batchClassName}", ex);
                    throw;
                }
            }
        }


        // Get page types for a batch class from API_page_type
        public async Task<List<string>> GetPageTypesAsync(string batchClassName)
        {
            using (var context = CreateContext())
            {
                try
                {
                    LoggerHelper.LogInfo($"Retrieving page types for batch class: {batchClassName}"); // Log query attempt
                    var results = await context.ApiPageTypes
                        .Where(pt => pt.IdDocumentClass == context.ApiDocumentClasses
                            .Where(dc => dc.IdBatchClass == context.ApiBatchClasses
                                .Where(bc => bc.Name == batchClassName)
                                .Select(bc => bc.IdBatchClass)
                                .FirstOrDefault())
                            .Select(dc => dc.IdDocumentClass)
                            .FirstOrDefault())
                        .Select(pt => pt.Name)
                        .OrderBy(name => name)
                        .ToListAsync();
                    LoggerHelper.LogInfo($"Retrieved {results.Count} page types for batch class: {batchClassName}"); // Log successful retrieval
                    return results;
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError($"Failed to retrieve page types for batch class: {batchClassName}", ex); // Log error
                    throw;
                }
            }
        }


        // Get field type for a field from API_field_type and API_batch_field_def
        public async Task<string> GetFieldTypeAsync(string fieldName)
        {
            using (var context = CreateContext())
            {
                try
                {
                    LoggerHelper.LogInfo($"Retrieving field type for field: {fieldName}"); // Log query attempt
                    var fieldType = await context.ApiBatchFieldDefs
                        .Where(bfd => bfd.FieldName == fieldName)
                        .Select(bfd => bfd.IdFieldTypeNavigation.TypeName)
                        .FirstOrDefaultAsync();
                    LoggerHelper.LogInfo($"Retrieved field type: {fieldType ?? "None"} for field: {fieldName}"); // Log successful retrieval
                    return fieldType ?? string.Empty;
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError($"Failed to retrieve field type for field: {fieldName}", ex); // Log error
                    throw;
                }
            }
        }


        public async Task<int> SaveGroupAsync(string groupName, bool isSubmitted)
        {
            using (var context = CreateContext())
            {
                try
                {
                    LoggerHelper.LogInfo($"Saving group: {groupName}, IsSubmitted: {isSubmitted}"); // Log group save attempt
                    var group = new Group
                    {
                        GroupName = groupName,
                        IsSubmitted = isSubmitted,
                        CreatedAt = DateTime.Now
                    };
                    context.Groups.Add(group);
                    await context.SaveChangesAsync();
                    LoggerHelper.LogInfo($"Saved group with ID: {group.GroupId}"); // Log successful save
                    return group.GroupId;
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError($"Failed to save group: {groupName}", ex); // Log error
                    throw;
                }
            }
        }

        public async Task<int> SaveSubmissionAsync(int groupId, string batchClassName, string sourceSystem, string channel, string sessionId, string messageId, string userCode, string interactionDateTime, string requestGuid, string authToken)
        {
            using (var context = CreateContext())
            {
                try
                {
                    LoggerHelper.LogInfo($"Saving submission for Group ID: {groupId}, Request GUID: {requestGuid}"); // Log submission save attempt
                    var submission = new Submission
                    {
                        GroupId = groupId,
                        BatchClassName = batchClassName,
                        SourceSystem = sourceSystem,
                        Channel = channel,
                        SessionId = sessionId,
                        MessageId = messageId,
                        UserCode = userCode,
                        InteractionDateTime = DateTime.Parse(interactionDateTime),
                        RequestGuid = requestGuid,
                        AuthToken = authToken,
                        SubmittedAt = DateTime.Now
                    };
                    context.Submissions.Add(submission);
                    await context.SaveChangesAsync();
                    LoggerHelper.LogInfo($"Saved submission with ID: {submission.SubmissionId}"); // Log successful save
                    return submission.SubmissionId;
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError($"Failed to save submission for Group ID: {groupId}, Request GUID: {requestGuid}", ex); // Log error
                    throw;
                }
            }
        }

        public async Task SaveDocumentAsync(int submissionId, string filePath, string pageType, string fileName)
        {
            using (var context = CreateContext())
            {
                try
                {
                    LoggerHelper.LogInfo($"Saving document for Submission ID: {submissionId}, File: {fileName}"); // Log document save attempt
                    var document = new Document
                    {
                        SubmissionId = submissionId,
                        FilePath = filePath,
                        PageType = pageType,
                        FileName = fileName,
                        CreatedAt = DateTime.Now
                    };
                    context.Documents.Add(document);
                    await context.SaveChangesAsync();
                    LoggerHelper.LogInfo($"Saved document for Submission ID: {submissionId}, File: {fileName}"); // Log successful save
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError($"Failed to save document for Submission ID: {submissionId}, File: {fileName}", ex); // Log error
                    throw;
                }
            }
        }

        public async Task SaveFieldAsync(int submissionId, string fieldName, string fieldValue, string fieldType)
        {
            using (var context = CreateContext())
            {
                try
                {
                    LoggerHelper.LogInfo($"Saving field for Submission ID: {submissionId}, Field: {fieldName}"); // Log field save attempt
                    var field = new Field
                    {
                        SubmissionId = submissionId,
                        FieldName = fieldName,
                        FieldValue = fieldValue,
                        FieldType = fieldType,
                        CreatedAt = DateTime.Now
                    };
                    context.Fields.Add(field);
                    await context.SaveChangesAsync();
                    LoggerHelper.LogInfo($"Saved field for Submission ID: {submissionId}, Field: {fieldName}"); // Log successful save
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError($"Failed to save field for Submission ID: {submissionId}, Field: {fieldName}", ex); // Log error
                    throw;
                }
            }
        }

        public async Task<SubmissionDetailsModel> GetSubmissionDetailsAsync(string requestGuid)
        {
            using (var context = CreateContext())
            {
                try
                {
                    LoggerHelper.LogInfo($"Retrieving submission details for Request GUID: {requestGuid}"); // Log retrieval attempt
                    var submission = await context.Submissions
                        .Include(s => s.Group)
                        .Include(s => s.Documents)
                        .Include(s => s.Fields)
                        .FirstOrDefaultAsync(s => s.RequestGuid == requestGuid);

                    if (submission == null)
                    {
                        LoggerHelper.LogWarning($"No submission found for Request GUID: {requestGuid}"); // Log warning
                        return null;
                    }

                    var result = new SubmissionDetailsModel
                    {
                        Submission = submission,
                        GroupName = submission.Group?.GroupName,
                        Documents = submission.Documents.ToList(),
                        Fields = submission.Fields.ToList()
                    };
                    LoggerHelper.LogInfo($"Retrieved submission details for Request GUID: {requestGuid}, {result.Documents.Count} documents, {result.Fields.Count} fields"); // Log successful retrieval
                    return result;
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError($"Failed to retrieve submission details for Request GUID: {requestGuid}", ex); // Log error
                    throw;
                }
            }
        }

        public async Task<int> SaveVerificationResponseAsync(VerificationResponse verificationResponse)
        {
            using (var context = CreateContext())
            {
                try
                {
                    LoggerHelper.LogInfo($"Saving verification response for Request GUID: {verificationResponse.RequestGuid}"); // Log response save attempt
                    // Map VerificationResponse
                    var efVerificationResponse = new VerificationResponse
                    {
                        Status = verificationResponse.Status,
                        ExecutionDate = verificationResponse.ExecutionDate,
                        ErrorMessage = verificationResponse.ErrorMessage,
                        RequestGuid = verificationResponse.RequestGuid,
                        SourceSystem = verificationResponse.SourceSystem,
                        Channel = verificationResponse.Channel,
                        SessionId = verificationResponse.SessionId,
                        MessageId = verificationResponse.MessageId,
                        UserCode = verificationResponse.UserCode,
                        InteractionDateTime = verificationResponse.InteractionDateTime,
                        ResponseJson = verificationResponse.ResponseJson
                    };

                    // Map Batch
                    if (verificationResponse.Batch != null)
                    {
                        var efBatch = new Batch
                        {
                            Name = verificationResponse.Batch.Name,
                            CreationDate = verificationResponse.Batch.CreationDate,
                            CloseDate = verificationResponse.Batch.CloseDate
                        };

                        // Map BatchClass
                        if (verificationResponse.Batch.BatchClass != null)
                        {
                            var existingBatchClass = await context.BatchClasses
                                .FirstOrDefaultAsync(bc => bc.Name == verificationResponse.Batch.BatchClass.Name);
                            if (existingBatchClass == null)
                            {
                                efBatch.BatchClass = new BatchClass { Name = verificationResponse.Batch.BatchClass.Name };
                            }
                            else
                            {
                                efBatch.BatchClassId = existingBatchClass.BatchClassId;
                            }
                        }

                        // Map BatchFields
                        efBatch.BatchFields = verificationResponse.Batch.BatchFields?.Select(bf => new BatchField
                        {
                            Name = bf.Name,
                            Value = bf.Value,
                            Confidence = bf.Confidence
                        }).ToList() ?? new List<BatchField>();

                        // Map BatchStates
                        efBatch.BatchStates = verificationResponse.Batch.BatchStates?.Select(bs => new BatchState
                        {
                            Value = bs.Value,
                            TrackDate = bs.TrackDate,
                            Workstation = bs.Workstation
                        }).ToList() ?? new List<BatchState>();

                        // Map Documents
                        efBatch.VerificationDocuments = verificationResponse.Batch.VerificationDocuments?.Select(doc => new VerificationDocument
                        {
                            Name = doc.Name,
                            // Map DocumentClass
                            DocumentClass = doc.DocumentClass != null ? new VerificationDocumentClass { Name = doc.DocumentClass.Name } : null,
                            // Map Pages
                            Pages = doc.Pages?.Select(p => new Page
                            {
                                FileName = p.FileName,
                                Sections = p.Sections != null ? JsonSerializer.Serialize(p.Sections) : null,
                                PageTypes = p.PageTypes?.Select(pt => new PageType
                                {
                                    Name = pt.Name,
                                    Confidence = pt.Confidence
                                }).ToList() ?? new List<PageType>()
                            }).ToList() ?? new List<Page>(),
                            // Map DocumentFields
                            DocumentFields = doc.DocumentFields?.Select(df =>
                            {
                                try
                                {
                                    var field = df as dynamic;
                                    return new DocumentField
                                    {
                                        Name = field?.Name?.ToString() ?? "Unknown",
                                        Value = field?.Value?.ToString(),
                                        Confidence = field?.Confidence
                                    };
                                }
                                catch
                                {
                                    // Fallback for malformed fields
                                    LoggerHelper.LogWarning($"Malformed document field for Request GUID: {verificationResponse.RequestGuid}"); // Log warning
                                    return new DocumentField
                                    {
                                        Name = "Unknown",
                                        Value = JsonSerializer.Serialize(df),
                                        Confidence = null
                                    };
                                }
                            }).ToList() ?? new List<DocumentField>(),
                            // Map Signatures
                            Signatures = doc.Signatures?.Select(s => new Signature
                            {
                                SignatureData = JsonSerializer.Serialize(s)
                            }).ToList() ?? new List<Signature>()
                        }).ToList() ?? new List<VerificationDocument>();

                        efVerificationResponse.Batch = efBatch;
                    }

                    context.VerificationResponses.Add(efVerificationResponse);
                    await context.SaveChangesAsync();
                    LoggerHelper.LogInfo($"Saved verification response with ID: {efVerificationResponse.VerificationResponseId}"); // Log successful save
                    return efVerificationResponse.VerificationResponseId;
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError($"Failed to save verification response for Request GUID: {verificationResponse.RequestGuid}", ex); // Log error
                    throw;
                }
            }
        }

        public async Task<bool> UpdateCheckedGuidAsync(string requestGuid)
        {
            using (var context = CreateContext())
            {
                try
                {
                    LoggerHelper.LogInfo($"Updating CheckedGuid for Request GUID: {requestGuid}"); // Log update attempt
                    var submission = await context.Submissions
                        .FirstOrDefaultAsync(s => s.RequestGuid == requestGuid);

                    if (submission == null)
                    {
                        LoggerHelper.LogWarning($"No submission found for Request GUID: {requestGuid}"); // Log warning
                        return false; // Submission not found
                    }

                    submission.CheckedGuid = true;
                    await context.SaveChangesAsync();
                    LoggerHelper.LogInfo($"Updated CheckedGuid for Request GUID: {requestGuid}"); // Log successful update
                    return true; // Update successful
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError($"Failed to update CheckedGuid for Request GUID: {requestGuid}", ex); // Log error
                    throw;
                }
            }
        }

        public async Task<List<string>> GetUncheckedRequestGuidsAsync()
        {
            using (var context = CreateContext())
            {
                try
                {
                    LoggerHelper.LogInfo("Retrieving unchecked Request GUIDs"); // Log retrieval attempt
                    var guids = await context.Submissions
                        .Where(s => !s.CheckedGuid)
                        .Select(s => s.RequestGuid)
                        .ToListAsync();
                    LoggerHelper.LogInfo($"Retrieved {guids.Count} unchecked Request GUIDs"); // Log successful retrieval
                    return guids;
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError("Failed to retrieve unchecked Request GUIDs", ex); // Log error
                    throw;
                }
            }
        }

        public async Task<List<VerificationResponse>> GetAllVerificationResponses()
        {
            using (var context = CreateContext())
            {
                try
                {
                    LoggerHelper.LogInfo("Retrieving all verification responses"); // Log retrieval attempt
                    var efVerificationResponses = await context.VerificationResponses
                        .ToListAsync();
                    LoggerHelper.LogInfo($"Retrieved {efVerificationResponses.Count} verification responses"); // Log successful retrieval
                    return efVerificationResponses;
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError("Failed to retrieve all verification responses", ex); // Log error
                    throw;
                }
            }
        }

        public async Task<List<VerificationResponseModel>> GetFilteredVerificationResponses(
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? status = null,
            string? sourceSystem = null,
            string? channel = null,
            string? requestGuid = null,
            string? batchClassName = null,
            string? sessionId = null,
            string? messageId = null,
            string? userCode = null)
        {
            using (var context = CreateContext())
            {
                try
                {
                    LoggerHelper.LogInfo("Retrieving filtered verification responses"); // Log retrieval attempt
                    var query = context.VerificationResponses
                        .Include(vr => vr.Batch)
                        .ThenInclude(b => b.BatchClass)
                        .AsQueryable();

                    // Normalize dates to handle same-day filtering
                    if (startDate.HasValue)
                    {
                        var start = startDate.Value.Date; // Midnight of start date
                        query = query.Where(vr => vr.InteractionDateTime >= start);
                    }

                    if (endDate.HasValue)
                    {
                        var end = endDate.Value.Date.AddDays(1).AddTicks(-1); // End of end date
                        query = query.Where(vr => vr.InteractionDateTime <= end);
                    }

                    if (status.HasValue)
                        query = query.Where(vr => vr.Status == status.Value);

                    // Partial text search for text-based filters (case-insensitive using ToLower)
                    if (!string.IsNullOrEmpty(sourceSystem))
                        query = query.Where(vr => vr.SourceSystem != null && vr.SourceSystem.ToLower().Contains(sourceSystem.ToLower()));

                    if (!string.IsNullOrEmpty(channel))
                        query = query.Where(vr => vr.Channel != null && vr.Channel.ToLower().Contains(channel.ToLower()));

                    if (!string.IsNullOrEmpty(requestGuid))
                        query = query.Where(vr => vr.RequestGuid != null && vr.RequestGuid.ToLower().Contains(requestGuid.ToLower()));

                    if (!string.IsNullOrEmpty(batchClassName))
                        query = query.Where(vr => vr.Batch != null && vr.Batch.BatchClass != null &&
                            vr.Batch.BatchClass.Name != null &&
                            vr.Batch.BatchClass.Name.ToLower().Contains(batchClassName.ToLower()));

                    if (!string.IsNullOrEmpty(sessionId))
                        query = query.Where(vr => vr.SessionId != null && vr.SessionId.ToLower().Contains(sessionId.ToLower()));

                    if (!string.IsNullOrEmpty(messageId))
                        query = query.Where(vr => vr.MessageId != null && vr.MessageId.ToLower().Contains(messageId.ToLower()));

                    if (!string.IsNullOrEmpty(userCode))
                        query = query.Where(vr => vr.UserCode != null && vr.UserCode.ToLower().Contains(userCode.ToLower()));

                    var efVerificationResponses = await query
                        .Select(vr => new VerificationResponseModel
                        {
                            InteractionDateTime = vr.InteractionDateTime,
                            RequestGuid = vr.RequestGuid,
                            Status = vr.Status == 0 ? "OK" : "KO",
                            BatchClassName = vr.Batch != null && vr.Batch.BatchClass != null
                                ? vr.Batch.BatchClass.Name
                                : null,
                            SourceSystem = vr.SourceSystem,
                            Channel = vr.Channel,
                            SessionId = vr.SessionId,
                            MessageId = vr.MessageId,
                            UserCode = vr.UserCode,
                            ResponseJson = vr.ResponseJson
                        })
                        .ToListAsync();

                    LoggerHelper.LogInfo($"Retrieved {efVerificationResponses.Count} filtered verification responses"); // Log successful retrieval
                    return efVerificationResponses;
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError("Failed to retrieve filtered verification responses", ex); // Log error
                    throw;
                }
            }
        }
    }
}