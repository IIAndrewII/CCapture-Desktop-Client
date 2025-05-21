using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class ApiDocumentClass
{
    public int IdDocumentClass { get; set; }

    public int IdBatchClass { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ApiPageType> ApiPageTypes { get; set; } = new List<ApiPageType>();

    public virtual ApiBatchClass IdBatchClassNavigation { get; set; } = null!;
}
