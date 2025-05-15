using System;
using System.Collections.Generic;

namespace Konecta.Tools.CCaptureClient.Core.ApiEntities
{
    public class VerificationResponse
    {
        public Batch Batch { get; set; }
        public int Status { get; set; }
        public DateTime ExecutionDate { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Batch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime CloseDate { get; set; }
        public BatchClass BatchClass { get; set; }
        public List<BatchField> BatchFields { get; set; }
        public List<VerificationDocument> Documents { get; set; }
        public List<BatchState> BatchStates { get; set; }
    }

    public class BatchClass
    {
        public string Name { get; set; }
    }

    public class BatchField
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public float Confidence { get; set; }
    }

    public class VerificationDocument
    {
        public string Name { get; set; }
        public List<Page> Pages { get; set; }
        public VerificationDocumentClass DocumentClass { get; set; }
        public List<object> DocumentFields { get; set; }
        public List<object> Signatures { get; set; }
    }

    public class VerificationDocumentClass
    {
        public string Name { get; set; }
    }

    public class Page
    {
        public string FileName { get; set; }
        public List<PageType> PageTypes { get; set; }
        public object Sections { get; set; }
    }

    public class PageType
    {
        public string Name { get; set; }
        public float Confidence { get; set; }
    }

    public class BatchState
    {
        public string Value { get; set; }
        public DateTime TrackDate { get; set; }
        public string Workstation { get; set; }
    }
}