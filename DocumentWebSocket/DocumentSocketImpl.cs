using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO.DocumentsWS;
using DocMarkingSystemContracts.Interfaces;
using Newtonsoft.Json;
using WebSocketInfraContracts;
using Document = DocMarkingSystemContracts.DTO.Documents.Document;

namespace DocumentWebSocket
{
    [Register(Policy.Singelton,typeof(IDocumentWebSocket))]
    public class DocumentSocketImpl : IDocumentWebSocket
    {
        private ConcurrentDictionary<string, List<string>> _viewers = new ConcurrentDictionary<string, List<string>>();
        public ISocketHandlerInfra Handler { get; set; }

        public DocumentSocketImpl(ISocketHandlerInfra handler)
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

        public async Task SendAddDocument(Document document)
        {
            NewDocumentResponse response = new NewDocumentResponse() { Document = document};
            string data = JsonConvert.SerializeObject(response);
            await Handler.SendMessageToAll(data);
        }

        public async Task SendRemoveDocument(string docID)
        {
            RemoveDocumentResponse response = new RemoveDocumentResponse(){DocID = docID};
            string data = JsonConvert.SerializeObject(response);
            await Handler.SendMessageToAll(data);
        }

        public async Task onConnected(WebSocket socket, string id)
        {
            await Handler.onConnected(socket,id);
        }
    }
}
