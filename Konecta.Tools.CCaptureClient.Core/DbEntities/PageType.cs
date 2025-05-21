using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class PageType
{
    public int IdPageType { get; set; }

    public int IdDocumentClass { get; set; }

    public string Name { get; set; } = null!;

    public virtual DocumentClass IdDocumentClassNavigation { get; set; } = null!;
}
