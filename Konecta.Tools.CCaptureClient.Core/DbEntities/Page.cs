using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class Page
{
    public int PageId { get; set; }

    public int VerificationDocumentId { get; set; }

    public string FileName { get; set; } = null!;

    public string? Sections { get; set; }

    public virtual ICollection<PageType1> PageType1s { get; set; } = new List<PageType1>();

    public virtual VerificationDocument VerificationDocument { get; set; } = null!;
}
