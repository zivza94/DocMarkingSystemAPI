using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Markers
{
    public class RemoveMarkerResponseInvalidID:RemoveMarkerResponse
    {
        public RemoveMarkerRequest Request { get; set; }

        public RemoveMarkerResponseInvalidID(RemoveMarkerRequest request)
        {
            Request = request;
        }
    }
}
