using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Threading;
using DIContract;
using WebSocketInfraContracts;

namespace WebSocketInfra
{
    [Register(Policy.Transient,typeof(IConnectionManagerInfra))]
    public class ConnectionManagerInfra : IConnectionManagerInfra
    {
        private ConcurrentDictionary<string, WebSocket> _connections = new ConcurrentDictionary<string, WebSocket>();
        public void AddSocket(WebSocket socket)
        {
            string id = Guid.NewGuid().ToString();
            _connections.TryAdd(id, socket);
        }

        public IEnumerable<WebSocket> GetAllConnections()
        {
            return _connections.Values;
        }

        public string GetID(WebSocket socket)
        {
            string id = _connections.FirstOrDefault(x => x.Value == socket).Key;
            return id;
        }

        public WebSocket GetSocketByID(string id)
        {
            WebSocket socket = _connections.FirstOrDefault(x => x.Key == id).Value;
            return socket;
        }

        public async Task RemoveSocketAsync(string id)
        {
            _connections.TryRemove(id, out var socket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "socket connection closed",
                CancellationToken.None);
        }
    }
}
