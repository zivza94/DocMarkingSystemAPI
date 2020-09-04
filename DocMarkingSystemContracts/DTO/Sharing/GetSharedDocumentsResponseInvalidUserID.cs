using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Sharing
{
    public class GetSharedDocumentsResponseInvalidUserID:GetSharedDocumentsResponse
    {
        public GetSharedDocumentsRequest Request { get; }

        public GetSharedDocumentsResponseInvalidUserID(GetSharedDocumentsRequest request)
        {
            Request = request;
        }

    }
}
