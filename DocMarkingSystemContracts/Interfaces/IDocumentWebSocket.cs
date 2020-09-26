using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using DocMarkingSystemContracts.DTO.Documents;
using DocMarkingSystemContracts.DTO.Markers;
using WebSocketInfraContracts;

namespace DocMarkingSystemContracts.Interfaces
{
    public interface IDocumentWebSocket
    {
        public ISocketHandlerInfra Handler { get; set; }

        public Task onConnected(WebSocket socket);
        public Task onConnected(WebSocket socket,string id);

        public Task onDisconnected(WebSocket socket);
        public Task Notify(string message);
        public Task SendAddDocument(Document document);
        public Task SendRemoveDocument(string docID);
    }
}
