using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO.DocumentsWS;
using DocMarkingSystemContracts.Interfaces;
using Newtonsoft.Json;
using WebSocketInfraContracts;

namespace ViewWebSocket
{
    [Register(Policy.Singelton,typeof(IViewWebSocket))]
    public class ViewWebSocketImpl : IViewWebSocket
    {
        private ConcurrentDictionary<string, List<string>> _viewers = new ConcurrentDictionary<string, List<string>>();
        public ISocketHandlerInfra Handler { get; set; }

        public ViewWebSocketImpl(ISocketHandlerInfra handler)
        {
            Handler = handler;
        }
        

        public async Task onConnected(WebSocket socket, string id, string docId)
        {
            await Handler.onConnected(socket, id);
            await AddViewer(docId, id);
        }

        public async Task onDisconnected(WebSocket socket, string id, string docId)
        {
            await Handler.onDisconnected(socket);
            await RemoveViewer(docId, id);
        }
        private async Task AddViewer(string docID, string userID)
        {
            var users = new List<string>();
            var exists = _viewers.TryGetValue(docID, out users);
            if (!exists)
            {
                users = new List<string>();
                _viewers.TryAdd(docID, users);
            }
            users.Add(userID);
            await SendCurrentlyViewing(users);
        }
        private async Task RemoveViewer(string docID, string userID)
        {
            _viewers[docID].Remove(userID);
            await SendCurrentlyViewing(_viewers[docID]);
        }

        private async Task SendCurrentlyViewing(List<string> users)
        {
            CurrentlyViewingResponse response = new CurrentlyViewingResponse() { Users = users };
            string data = JsonConvert.SerializeObject(response);
            foreach (string id in users)
            {
                await Handler.SendMessage(await Handler.GetSocketByID(id), data);
            }
        }
    }
}
