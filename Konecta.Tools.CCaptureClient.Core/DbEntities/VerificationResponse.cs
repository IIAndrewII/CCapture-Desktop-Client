using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class VerificationResponse
{
    public int VerificationResponseId { get; set; }

    public int? BatchId { get; set; }

    public int Status { get; set; }

    public DateTime ExecutionDate { get; set; }

    public string? ErrorMessage { get; set; }

    public string? RequestGuid { get; set; }

    public string? SourceSystem { get; set; }

    public string? Channel { get; set; }

    public string? SessionId { get; set; }

    public string? MessageId { get; set; }

    public string? UserCode { get; set; }

    public DateTime? InteractionDateTime { get; set; }

    public string? ResponseJson { get; set; }

    public virtual Batch? Batch { get; set; }
}
