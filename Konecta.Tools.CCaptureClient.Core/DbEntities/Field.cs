using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class Field
{
    public int FieldId { get; set; }

    public int SubmissionId { get; set; }

    public string FieldName { get; set; } = null!;

    public string FieldValue { get; set; } = null!;

    public string? FieldType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Submission Submission { get; set; } = null!;
}
