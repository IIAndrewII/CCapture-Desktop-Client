using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class ApiBatchClass
{
    public int IdBatchClass { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ApiBatchFieldDef> ApiBatchFieldDefs { get; set; } = new List<ApiBatchFieldDef>();

    public virtual ICollection<ApiDocumentClass> ApiDocumentClasses { get; set; } = new List<ApiDocumentClass>();
}
