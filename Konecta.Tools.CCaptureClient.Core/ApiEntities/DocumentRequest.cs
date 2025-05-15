using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konecta.Tools.CCaptureClient.Core.ApiEntities
{

    public class DocumentRequest
    {
        public string BatchClassName { get; set; }
        public List<Field> Fields { get; set; }
        public List<Document> Documents { get; set; }
        public string SourceSystem { get; set; }
        public string Channel { get; set; }
        public string InteractionDateTime { get; set; }
        public string SessionID { get; set; }
        public string MessageID { get; set; }
        public string UserCode { get; set; }
    }
}
