using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Markers
{
    public class CreateMarkerResponseInvalidMarkerType : CreateMarkerResponse
    {
        public CreateMarkerRequest Request { get; }

        public CreateMarkerResponseInvalidMarkerType(CreateMarkerRequest request)
        {
            Request = request;
        }
    }
}
