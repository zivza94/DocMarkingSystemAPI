using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Documents
{
    public class GetDocumentResponseInvalidDocID:GetDocumentResponse
    {
        public GetDocumentResponseInvalidDocID(GetDocumentRequest request)
        {
            Request = request;
        }

        public GetDocumentRequest Request { get; }
    }
}
