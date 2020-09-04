using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Documents
{
    public class GetDocumentResponseOK: GetDocumentResponse
    {
        public Document Document { get; }
        public GetDocumentResponseOK(Document document)
        {
            Document = document;
        }
    }
}
