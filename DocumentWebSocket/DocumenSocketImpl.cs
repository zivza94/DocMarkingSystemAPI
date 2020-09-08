using System;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.Interfaces;
using Newtonsoft.Json;
using WebSocketInfraContracts;
using Document = DocMarkingSystemContracts.DTO.Documents.Document;

namespace DocumentWebSocket
{
    [Register(Policy.Transient,typeof(IDocumentWebSocket))]
    public class DocumenSocketImpl : IDocumentWebSocket
    {
        public ISocketHandlerInfra Handler { get; set; }

        public DocumenSocketImpl(ISocketHandlerInfra handler)
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

        public async Task SendDocument(Document document)
        {
            var JsonDocument = JsonConvert.SerializeObject(document);
            await Handler.SendMessageToAll(JsonDocument);
        }
    }
}
