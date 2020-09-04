using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Documents
{
    public class RemoveDocumentResponseOK: RemoveDocumentResponse
    {
        public RemoveDocumentResponseOK(RemoveDocumentRequest request)
        {
            Request = request;
        }

        public RemoveDocumentRequest Request { get;}
    }
}
