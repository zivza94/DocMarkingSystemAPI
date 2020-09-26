using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using DocMarkingSystemContracts.DTO.LiveDrawWS;
using DocMarkingSystemContracts.DTO.MarkersWS;

namespace DocMarkingSystemContracts.Interfaces
{
    public interface ILiveDrawWebSocket
    {
        public Task onConnected(WebSocket socket, string id, string docId);
        public Task onDisconnected(WebSocket socket, string id, string docId);
        public Task Receive(WebSocket socket, string id, string docID,byte[] buffer);
        public Task UpdateAll(List<string> users, LiveDrawResponse response);
    }
}
