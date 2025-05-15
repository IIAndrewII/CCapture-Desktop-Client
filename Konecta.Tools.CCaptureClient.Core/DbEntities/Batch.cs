using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class Batch
{
    public int BatchId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime CloseDate { get; set; }

    public int? BatchClassId { get; set; }

    public virtual BatchClass? BatchClass { get; set; }

    public virtual ICollection<BatchField> BatchFields { get; set; } = new List<BatchField>();

    public virtual ICollection<BatchState> BatchStates { get; set; } = new List<BatchState>();

    public virtual ICollection<VerificationDocument> VerificationDocuments { get; set; } = new List<VerificationDocument>();

    public virtual ICollection<VerificationResponse> VerificationResponses { get; set; } = new List<VerificationResponse>();
}
