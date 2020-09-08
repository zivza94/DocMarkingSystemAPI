using DocMarkingSystemContracts.DTO.Markers;
using DocMarkingSystemContracts.Interfaces;
using System;
using System.Net.WebSockets;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DIContract;
using WebSocketInfraContracts;
using Newtonsoft.Json;

namespace MarkerWebSocket
{
    [Register(Policy.Singelton,typeof(IMarkerWebSocket))]
    public class MarkerWebSocketImpl : IMarkerWebSocket
    {
        public ISocketHandlerInfra Handler { get ; set; }

        public MarkerWebSocketImpl(ISocketHandlerInfra handler)
        {
            Handler = handler;
        }
        public async Task onConnected(WebSocket socket)
        {
            await Handler.onConnected(socket);
        }

        public async Task onDisconnected(WebSocket socket)
        {
            await Handler.onDisconnected(socket);
        }

        public async Task Notify(string message)
        {
            await Handler.SendMessageToAll(message);
        }
    }
}
