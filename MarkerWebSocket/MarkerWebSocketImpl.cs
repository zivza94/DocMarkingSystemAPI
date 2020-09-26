using DocMarkingSystemContracts.DTO.Markers;
using DocMarkingSystemContracts.Interfaces;
using System;
using System.Net.WebSockets;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO.MarkersWS;
using WebSocketInfraContracts;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using RemoveMarkerResponse = DocMarkingSystemContracts.DTO.MarkersWS.RemoveMarkerResponse;

namespace MarkerWebSocket
{
    [Register(Policy.Singelton,typeof(IMarkerWebSocket))]
    public class MarkerWebSocketImpl : IMarkerWebSocket
    {
        public ISocketHandlerInfra Handler { get ; set; }

        public MarkerWebSocketImpl(ISocketHandlerInfra handler)
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

        public async Task SendNewMarker(Marker marker)
        {
            NewMarkerResponse response = new NewMarkerResponse(){Marker =  marker};
            string message = JsonConvert.SerializeObject(response);
            await Handler.SendMessageToAll(message);
        }

        public  async Task SendRemoveMarker(string markerID)
        {
            RemoveMarkerResponse response = new RemoveMarkerResponse() { MarkerID = markerID};
            string message = JsonConvert.SerializeObject(response);
            await Handler.SendMessageToAll(message);
        }
    }
}
