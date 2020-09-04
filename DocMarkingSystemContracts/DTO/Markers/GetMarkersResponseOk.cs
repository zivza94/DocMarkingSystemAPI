using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Markers
{
    public class GetMarkersResponseOk:GetMarkersResponse
    {
        public List<Marker> Markers { get; }

        public GetMarkersResponseOk(List<Marker> markers)
        {
            Markers = markers;
        }
    }
}
