using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO.LiveDrawWS;
using DocMarkingSystemContracts.Interfaces;
using Newtonsoft.Json;
using WebSocketInfraContracts;

namespace LiveDrawWebSocket
{
    [Register(Policy.Singelton, typeof(ILiveDrawWebSocket))]
    public class LiveDrawWebSocketImpl : ILiveDrawWebSocket
    {
        public ISocketHandlerInfra Handler{ get; set; }
        private ConcurrentDictionary<string,List<string>> _users = new ConcurrentDictionary<string, List<string>>();
        private ConcurrentDictionary<string,List<Line>> _lines = new ConcurrentDictionary<string, List<Line>>();
        public LiveDrawWebSocketImpl(ISocketHandlerInfra handler)
        {
            Handler = handler;
        }
        public async Task onConnected(WebSocket socket, string id, string docId)
        {
            await Handler.onConnected(socket, id);
            AddViewer(docId, id);

        }

        public async Task onDisconnected(WebSocket socket, string id, string docId)
        {
            await Handler.onDisconnected(socket);
        }

        public async Task Receive(WebSocket socket, string id, string docID, byte[] buffer)
        {
            try
            {
                var req = JsonConvert.DeserializeObject<LiveDrawRequest>(Encoding.UTF8.GetString(buffer));
                if (req.RequestType == typeof(EndLiveDrawRequest).Name)
                {
                    var response = new EndLiveDrawResponse() {UserID = id};
                    _lines.TryRemove(id, out var lines);
                    await UpdateAll(_users[docID], response);
                }
                else if (req.RequestType == typeof(NewLiveDrawRequest).Name)
                {
                    var newLineReq = JsonConvert.DeserializeObject<NewLiveDrawRequest>(Encoding.UTF8.GetString(buffer));
                    await AddLine(id, newLineReq.Line);
                    var response = new NewLiveDrawResponse() {Lines = _lines[id], UserID = id};
                    await UpdateAll(_users[docID], response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task UpdateAll(List<string> users, LiveDrawResponse response)
        {
            string data = JsonConvert.SerializeObject(response);
            foreach (string id in users)
            {
                await Handler.SendMessage(await Handler.GetSocketByID(id), data);
            }
        }
        private async Task AddViewer(string docID, string userID)
        {
            var users = new List<string>();
            var exists = _users.TryGetValue(docID, out users);
            if (!exists)
            {
                users = new List<string>();
                _users.TryAdd(docID, users);
            }
            users.Add(userID);
        }
        private async Task RemoveViewer(string docID, string userID)
        {
            _users[docID].Remove(userID);
            _lines.TryRemove(userID,out var lines);
        }

        private async Task AddLine(string userID, Line line)
        {
            var lines = new List<Line>();
            var exist = _lines.TryGetValue(userID,out lines);
            if (!exist)
            {
                lines = new List<Line>();
                _lines.TryAdd(userID, lines);
            }
            lines.Add(line);
        }
    }
}
