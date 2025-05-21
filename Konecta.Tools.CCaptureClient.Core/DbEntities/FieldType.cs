using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class FieldType
{
    public int IdFieldType { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<BatchFieldDef> BatchFieldDefs { get; set; } = new List<BatchFieldDef>();
}
