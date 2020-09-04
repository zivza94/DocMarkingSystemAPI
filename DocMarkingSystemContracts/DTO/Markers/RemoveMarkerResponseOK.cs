using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Markers
{
    public class RemoveMarkerResponseOK:RemoveMarkerResponse
    {
        public RemoveMarkerRequest Request { get;}

        public RemoveMarkerResponseOK(RemoveMarkerRequest request)
        {
            Request = request;
        }
    }
}
