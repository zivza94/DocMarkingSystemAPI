using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Documents
{
    public class CreateDocumentResponseInvalidData:CreateDocumentResponse
    {
        public CreateDocumentResponseInvalidData(CreateDocumentRequest request)
        {
            Request = request;
        }

        public CreateDocumentRequest Request { get; }
    }
}
