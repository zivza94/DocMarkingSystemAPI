using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO.Sharing;
using DocMarkingSystemContracts.Interfaces;
using Newtonsoft.Json;
using WebSocketInfraContracts;

namespace SharingWebSocket
{
    [Register(Policy.Transient, typeof(ISharingWebSocket))]
    public class SharingWebSocketImpl : ISharingWebSocket
    {
        public ISocketHandlerInfra Handler { get; set; }

        public SharingWebSocketImpl(ISocketHandlerInfra handler)
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
