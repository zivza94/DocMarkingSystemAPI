using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace DocMarkingSystemContracts.Interfaces
{
    public interface IViewWebSocket
    {
        public Task onConnected(WebSocket socket, string id, string docId);
        public Task onDisconnected(WebSocket socket,string id, string docId);

    }
}
