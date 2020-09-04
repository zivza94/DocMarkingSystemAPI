using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO.Markers;

namespace DocMarkingSystemContracts.Interfaces
{
    public interface IMarkerService
    {
        public void Connect(string connStr);
        public Task<Response> CreateMarker(CreateMarkerRequest request);
        public Task<Response> GetMarkers(GetMarkersRequest request);
        public Task<Response> RemoveMarker(RemoveMarkerRequest request);
    }
}
