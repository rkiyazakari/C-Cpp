using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using SharedProject.DTO;

namespace SharedProject
{
    public static class Communication
    {
        private static async void SendByte(WebSocket webSocket, byte[] resp)
        {
            await webSocket.SendAsync(new ArraySegment<byte>(resp, 0, resp.Length),
                WebSocketMessageType.Text, true, CancellationToken.None);
        }


        public static void SendResponse<T>(WebSocket webSocket, T commandData)
            where T : CommandDto
        {
            var response = new ServerSimpleResponse<T>(commandData);
            var responseBytes = response.ToByte();
            SendByte(webSocket, responseBytes);
        }

        public static void SendListResponse<T>(WebSocket webSocket, List<T> commandData)
            where T : CommandDto
        {
            var response = new ServerListResponse<T>(commandData);
            var responseBytes = response.ToByte();
            SendByte(webSocket, responseBytes);
        }


        public static void SendSuccess(WebSocket webSocket)
        {
            var responseBytes = ServerSimpleResponse.SuccessNoData().ToByte();
            SendByte(webSocket, responseBytes);
        }


        public static void SendError(WebSocket webSocket, string errorMessage)
        {
            var errorResp = new ServerSimpleResponse<InfoDto>(
                "error",
                new InfoDto(errorMessage)
            );
            SendByte(webSocket, errorResp.ToByte());
        }

        //
        // public static string ReceiveMsg(Stream s)
        // {
        //     var reader = new StreamReader(s);
        //     return reader.ReadLine();
        // }
    }
}