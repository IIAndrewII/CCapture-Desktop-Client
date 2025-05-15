using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class Group
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public bool IsSubmitted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
