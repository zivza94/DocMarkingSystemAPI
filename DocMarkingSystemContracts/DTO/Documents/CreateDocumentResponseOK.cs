using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Documents
{
    public class CreateDocumentResponseOK:CreateDocumentResponse
    {
        public CreateDocumentResponseOK(CreateDocumentRequest request)
        {
            Request = request;
        }

        public CreateDocumentRequest Request { get; }
    }
}
