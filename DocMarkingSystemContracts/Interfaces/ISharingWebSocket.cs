using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using DocMarkingSystemContracts.DTO.Markers;
using DocMarkingSystemContracts.DTO.Sharing;
using WebSocketInfraContracts;

namespace DocMarkingSystemContracts.Interfaces
{
    public interface ISharingWebSocket
    {
        public ISocketHandlerInfra Handler { get; set; }

        public Task onConnected(WebSocket socket);

        public Task onDisconnected(WebSocket socket);
        public Task SendShare(Share share);
    }
}
