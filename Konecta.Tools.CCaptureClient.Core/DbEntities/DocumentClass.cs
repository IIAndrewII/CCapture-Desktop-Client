using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class DocumentClass
{
    public int IdDocumentClass { get; set; }

    public int IdBatchClass { get; set; }

    public string Name { get; set; } = null!;

    public virtual BatchClass IdBatchClassNavigation { get; set; } = null!;

    public virtual ICollection<PageType> PageTypes { get; set; } = new List<PageType>();
}
