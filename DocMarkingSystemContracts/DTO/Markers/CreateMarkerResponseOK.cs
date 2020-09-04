using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Markers
{
    public class CreateMarkerResponseOK:CreateMarkerResponse
    {
        public CreateMarkerRequest Request { get; set; }

        public CreateMarkerResponseOK(CreateMarkerRequest request)
        {
            Request = request;
        }
    }
}
