using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class BatchFieldDef
{
    public int IdBatchFieldDef { get; set; }

    public int IdBatchClass { get; set; }

    public string FieldName { get; set; } = null!;

    public int IdFieldType { get; set; }

    public virtual BatchClass IdBatchClassNavigation { get; set; } = null!;

    public virtual FieldType IdFieldTypeNavigation { get; set; } = null!;
}
