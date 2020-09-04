using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Markers
{
    public class CreateMarkerResponseInvalidUserID : CreateMarkerResponse
    {
        public CreateMarkerRequest Request { get; }

        public CreateMarkerResponseInvalidUserID(CreateMarkerRequest request)
        {
            Request = request;
        }
    }
}
