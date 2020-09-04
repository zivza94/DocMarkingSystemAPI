using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Documents
{
    public class RemoveDocumentResponseInvalidDocID: RemoveDocumentResponse
    {
        public RemoveDocumentResponseInvalidDocID(RemoveDocumentRequest request)
        {
            Request = request;
        }

        public RemoveDocumentRequest Request { get; }
    }
}
