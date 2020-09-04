using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Markers
{
    public class GetMarkersResponseInvalidDocID:GetMarkersResponse
    {
        public GetMarkersRequest Request { get; }

        public GetMarkersResponseInvalidDocID(GetMarkersRequest request)
        {
            Request = request;
        }
    }
}
