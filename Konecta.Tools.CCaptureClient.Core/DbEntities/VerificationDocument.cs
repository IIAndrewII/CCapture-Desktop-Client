using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class VerificationDocument
{
    public int VerificationDocumentId { get; set; }

    public int BatchId { get; set; }

    public string Name { get; set; } = null!;

    public int? DocumentClassId { get; set; }

    public virtual Batch Batch { get; set; } = null!;

    public virtual VerificationDocumentClass? DocumentClass { get; set; }

    public virtual ICollection<DocumentField> DocumentFields { get; set; } = new List<DocumentField>();

    public virtual ICollection<Page> Pages { get; set; } = new List<Page>();

    public virtual ICollection<Signature> Signatures { get; set; } = new List<Signature>();
}
