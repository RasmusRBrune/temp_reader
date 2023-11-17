using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using temperature_Server.Services;

namespace temperature_Server.Sockets
{
    public static class SocketExtensions
    {
        //public static async Task Echo(WebSocket webSocket)
        //{
        //    var buffer = new byte[1024 * 4];
        //    var receiveResult = await webSocket.ReceiveAsync(
        //        new ArraySegment<byte>(buffer), CancellationToken.None);

        //    while (!receiveResult.CloseStatus.HasValue)
        //    {
        //        await webSocket.SendAsync(
        //            new ArraySegment<byte>(buffer, 0, receiveResult.Count),
        //            receiveResult.MessageType,
        //            receiveResult.EndOfMessage,
        //            CancellationToken.None);

        //        receiveResult = await webSocket.ReceiveAsync(
        //            new ArraySegment<byte>(buffer), CancellationToken.None);
        //        if (receiveResult.MessageType == WebSocketMessageType.Text)
        //        {
        //            var message = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
        //            await ProcessTextMessage(webSocket, message);
        //        }
        //        else if (receiveResult.MessageType == WebSocketMessageType.Binary)
        //        {
        //            // Handle binary messages if needed
        //            await ProcessBinaryMessage(webSocket, buffer, receiveResult.Count);
        //        }
        //    }

        //    await webSocket.CloseAsync(
        //        receiveResult.CloseStatus.Value,
        //        receiveResult.CloseStatusDescription,
        //        CancellationToken.None);
        //}

        //private static Task ProcessBinaryMessage(WebSocket webSocket, byte[] buffer, int count, ITemperatureReaderDeviceService deviceService, ITemperatureReadingService readingService, IDeviceTimeLogService timeLogService)
        //{
        //    throw new NotImplementedException();
        //}

        //private static Task ProcessTextMessage(WebSocket webSocket, string message, ITemperatureReaderDeviceService deviceService, ITemperatureReadingService readingService, IDeviceTimeLogService timeLogService)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
