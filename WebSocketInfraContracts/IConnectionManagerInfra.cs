using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketInfraContracts
{
    public interface IConnectionManagerInfra
    {
        public void AddSocket(WebSocket socket, string id);
        public WebSocket GetSocketByID(string id);
        public IEnumerable<WebSocket> GetAllConnections();
        public string GetID(WebSocket socket);
        public Task RemoveSocketAsync(string id);
        public void AddSocket(WebSocket socket);

    }
}
