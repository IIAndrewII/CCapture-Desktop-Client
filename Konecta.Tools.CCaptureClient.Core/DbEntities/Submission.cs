using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class Submission
{
    public int SubmissionId { get; set; }

    public int GroupId { get; set; }

    public string BatchClassName { get; set; } = null!;

    public string SourceSystem { get; set; } = null!;

    public string Channel { get; set; } = null!;

    public string SessionId { get; set; } = null!;

    public string MessageId { get; set; } = null!;

    public string UserCode { get; set; } = null!;

    public DateTime InteractionDateTime { get; set; }

    public string RequestGuid { get; set; } = null!;

    public string? AuthToken { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public bool CheckedGuid { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<Field> Fields { get; set; } = new List<Field>();

    public virtual Group Group { get; set; } = null!;
}
