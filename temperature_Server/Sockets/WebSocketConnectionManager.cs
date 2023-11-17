using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace temperature_Server.Sockets
{
    public class WebSocketConnectionManager
    {
        private readonly ConcurrentDictionary<string, WebSocket> _connections = new ConcurrentDictionary<string, WebSocket>();

        public void AddWebSocket(string id, WebSocket socket)
        {
            _connections.TryAdd(id, socket);
        }

        public WebSocket GetWebSocketById(string id)
        {
            _connections.TryGetValue(id, out var socket);
            return socket;
        }

        public ConcurrentDictionary<string, WebSocket> GetAllWebSockets()
        {
            return _connections;
        }

        public string GetId(WebSocket socket)
        {
            foreach (var (id, connection) in _connections)
            {
                if (connection == socket)
                {
                    return id;
                }
            }

            return null;
        }
    }
}
