using System.Net.WebSockets;
using temperature_Server.Services;

namespace temperature_Server.Sockets
{
    public class TemperatureReaderWebSocketHandler
    {
        private readonly WebSocketConnectionManager _webSocketConnectionManager;
        private readonly ITemperatureReaderDeviceService deviceService;
        private readonly ITemperatureReadingService readingService;
        private readonly IDeviceTimeLogService timeLogService;

        public TemperatureReaderWebSocketHandler(WebSocketConnectionManager webSocketConnectionManager, ITemperatureReaderDeviceService deviceService, ITemperatureReadingService readingService, IDeviceTimeLogService timeLogService)
        {
            _webSocketConnectionManager = webSocketConnectionManager;
            this.deviceService = deviceService;
            this.readingService = readingService;
            this.timeLogService = timeLogService;
        }

        public async Task HandleWebSocket(HttpContext context, WebSocket webSocket)
        {
            // Assuming you have some mechanism to generate a unique ID for each client
            var clientId = Guid.NewGuid().ToString();

            _webSocketConnectionManager.AddWebSocket(clientId, webSocket);

            // Your custom logic...

            await ReceiveAsync(webSocket);
        }

        private async Task ReceiveAsync(WebSocket socket)
        {
            // Your custom logic...

            // Example: Echo the received message back to the client
            await SendMessageAsync(socket, "Message received");
        }

        private async Task SendMessageAsync(WebSocket socket, string message)
        {
            // Your custom logic to send a message to the client
        }
    }
}
