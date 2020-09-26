using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketInfraContracts
{
    public interface ISocketHandlerInfra
    {
        public IConnectionManagerInfra Connection { get; set; }

        public Task onConnected(WebSocket socket);
        public Task onConnected(WebSocket socket, string id);
        public Task onDisconnected(WebSocket socket);
        public Task onDisconnected(string id);
        public Task SendMessage(WebSocket socket, string message);
        public Task SendMessageToAll(string message);
        public Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
        public Task<WebSocket> GetSocketByID(string id);
    }
}
