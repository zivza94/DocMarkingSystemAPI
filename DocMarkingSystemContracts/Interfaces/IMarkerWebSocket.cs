using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using DocMarkingSystemContracts.DTO.Markers;
using WebSocketInfraContracts;

namespace DocMarkingSystemContracts.Interfaces
{
    public interface IMarkerWebSocket
    {
        public ISocketHandlerInfra Handler { get; set; }

        public Task onConnected(WebSocket socket);

        public Task onDisconnected(WebSocket socket);
        public Task SendNewMarker(Marker marker);
        public Task SendRemoveMarker(string markerID);
    }
}
