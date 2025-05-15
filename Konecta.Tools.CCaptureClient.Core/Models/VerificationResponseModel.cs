using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konecta.Tools.CCaptureClient.Core.Models
{
    public class VerificationResponseModel
    {
        public DateTime? InteractionDateTime { get; set; }
        public string? RequestGuid { get; set; }
        public string Status { get; set; }
        public string? BatchClassName { get; set; }
        public string? SourceSystem { get; set; }
        public string? Channel { get; set; }
        public string? SessionId { get; set; }
        public string? MessageId { get; set; }
        public string? UserCode { get; set; }
        public string? ResponseJson { get; set; }
    }
}
