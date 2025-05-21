using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class BatchClass
{
    public int IdBatchClass { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BatchFieldDef> BatchFieldDefs { get; set; } = new List<BatchFieldDef>();

    public virtual ICollection<DocumentClass> DocumentClasses { get; set; } = new List<DocumentClass>();
}
