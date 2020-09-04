using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Markers
{
    public class CreateMarkerResponseInvalidDocID:CreateMarkerResponse
    {
        public CreateMarkerRequest Request { get; }

        public CreateMarkerResponseInvalidDocID(CreateMarkerRequest request)
        {
            Request = request;
        }
    }
}
