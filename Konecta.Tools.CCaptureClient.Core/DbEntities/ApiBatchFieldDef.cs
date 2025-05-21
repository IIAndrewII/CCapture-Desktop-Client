using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class ApiBatchFieldDef
{
    public int IdBatchFieldDef { get; set; }

    public int IdBatchClass { get; set; }

    public string FieldName { get; set; } = null!;

    public int IdFieldType { get; set; }

    public virtual ApiBatchClass IdBatchClassNavigation { get; set; } = null!;

    public virtual ApiFieldType IdFieldTypeNavigation { get; set; } = null!;
}
