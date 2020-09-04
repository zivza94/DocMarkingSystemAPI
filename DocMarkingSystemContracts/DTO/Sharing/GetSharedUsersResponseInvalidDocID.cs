using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Sharing
{
    public class GetSharedUsersResponseInvalidDocID:GetSharedUsersResponse
    {
        public GetSharedUsersRequest Request { get; }

        public GetSharedUsersResponseInvalidDocID(GetSharedUsersRequest request)
        {
            Request = request;
        }
    }
}
