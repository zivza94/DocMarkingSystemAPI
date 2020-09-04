using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Documents
{
    public class GetDocumentsResponseInvalidUserID:GetDocumentsResponse
    {
        public GetDocumentsResponseInvalidUserID(GetDocumentsRequest request)
        {
            Request = request;
        }

        public GetDocumentsRequest Request { get;}
    }
}
