using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class BatchState
{
    public int BatchStateId { get; set; }

    public int BatchId { get; set; }

    public string Value { get; set; } = null!;

    public DateTime TrackDate { get; set; }

    public string? Workstation { get; set; }

    public virtual Batch Batch { get; set; } = null!;
}
