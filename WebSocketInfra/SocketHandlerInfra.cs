using System;
using System.Collections.Generic;
using System.Data;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DIContract;
using WebSocketInfraContracts;

namespace WebSocketInfra
{
    [Register(Policy.Transient,typeof(ISocketHandlerInfra))]
    public class SocketHandlerInfra: ISocketHandlerInfra
    {
        public IConnectionManagerInfra Connection { get; set; }

        public SocketHandlerInfra(IConnectionManagerInfra connection)
        {
            Connection = connection;
        }

        public async Task onConnected(WebSocket socket)
        {
            await Task.Run(() => { Connection.AddSocket(socket); });
        }

        public async Task onConnected(WebSocket socket, string id)
        {
            await Task.Run(() => { Connection.AddSocket(socket,id); });
        }

        public async Task onDisconnected(WebSocket socket)
        {
            await Connection.RemoveSocketAsync(Connection.GetID(socket));
        }
        public async Task onDisconnected(string id)
        {
            await Connection.RemoveSocketAsync(id);
        }

        public async Task SendMessage(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
            {
                return;
            }

            await socket.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(message), 0, message.Length),
                WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task SendMessageToAll(string message)
        {
            foreach (WebSocket socket in Connection.GetAllConnections())
            {
                await SendMessage(socket, message);
            }
        }

        public Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public async Task<WebSocket> GetSocketByID(string id)
        {
            return Connection.GetSocketByID(id);
        }
    }
}
