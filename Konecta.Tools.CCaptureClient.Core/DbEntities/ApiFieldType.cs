using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class ApiFieldType
{
    public int IdFieldType { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<ApiBatchFieldDef> ApiBatchFieldDefs { get; set; } = new List<ApiBatchFieldDef>();
}
