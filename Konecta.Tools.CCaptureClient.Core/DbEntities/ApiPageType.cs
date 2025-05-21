using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class ApiPageType
{
    public int IdPageType { get; set; }

    public int IdDocumentClass { get; set; }

    public string Name { get; set; } = null!;

    public virtual ApiDocumentClass IdDocumentClassNavigation { get; set; } = null!;
}
