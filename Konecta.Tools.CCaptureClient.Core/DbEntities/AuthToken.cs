using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class AuthToken
{
    [Key]
    public int TokenId { get; set; }

    [StringLength(500)]
    public string TokenValue { get; set; } = null!;

    [StringLength(100)]
    public string ApplicationName { get; set; } = null!;

    [StringLength(100)]
    public string AppLogin { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }
}
