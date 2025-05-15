using Konecta.Tools.CCaptureClient.Core.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konecta.Tools.CCaptureClient.Core.ApiEntities
{
    public class SubmissionDetails
    {
        public Submission Submission { get; set; }
        public string GroupName { get; set; }
        public List<Document> Documents { get; set; }
        public List<Field> Fields { get; set; }
    }
}
