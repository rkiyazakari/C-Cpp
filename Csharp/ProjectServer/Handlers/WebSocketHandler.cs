using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using ProjectServer.Models;
using Serilog;
using SharedProject;
using SharedProject.CommandUtils;
using SharedProject.DTO;
using SharedProject.Utils;

namespace ProjectServer.Handlers
{
    /// <summary>
    ///     Class that handle the websocket connection thru multiple handler functions
    /// </summary>
    internal partial class WebSocketHandler
    {
        private static readonly ConcurrentDictionary<User, WebSocket> _connectedClient =
            new ConcurrentDictionary<User, WebSocket>();

        private readonly WebSocket _webSocket;
        private User _user;

        public WebSocketHandler(WebSocketContext webSocketContext)
        {
            _webSocket = webSocketContext.WebSocket;
            _user = null;
        }

        public async void Receive()
        {
            try
            {
                var receiveBuffer = new byte[Constants.MaxByte];

                while (_webSocket.State == WebSocketState.Open)
                {
                    var receiveResult =
                        await _webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);

                    switch (receiveResult.MessageType)
                    {
                        case WebSocketMessageType.Close:
                            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                            break;
                        case WebSocketMessageType.Binary:
                            await _webSocket.CloseAsync(WebSocketCloseStatus.InvalidMessageType,
                                "Cannot accept binary frame",
                                CancellationToken.None);
                            break;
                        default:
                        {
                            var message =
                                Encoding.Default.GetString(receiveBuffer)
                                    .Replace("\0", string.Empty); // receiveBuffer.ToString();

                            var command = new Command(message);

                            Log.Information($"Received {command.Type} request");

                            Dispatch(command);
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Exception: {0}", e);
                await _webSocket.CloseAsync(WebSocketCloseStatus.InternalServerError, "", CancellationToken.None);
            }
            finally
            {
                Log.Information("One connection ended");
                _webSocket.Dispose();
            }
        }

        /// <summary>
        ///     Dispatcher function that act like a router for all the features
        /// </summary>
        /// <param name="command">The command sent by the user via websocket</param>
        /// <exception cref="NotImplementedException"></exception>
        private void Dispatch(Command command)
        {
            switch (command.Type)
            {
                case CommandString.Login:
                    HandleLogin(command.GetDeserializedData<LoginDto>());
                    break;

                case CommandString.Register:
                    HandleRegister(command.GetDeserializedData<LoginDto>());
                    break;

                case CommandString.DirectMessage:
                    HandleDirectMessage(command.GetDeserializedData<DirectMessageDto>());
                    break;

                case CommandString.Send:
                    HandleTopicMessage(command.GetDeserializedData<TopicMessageDto>());
                    break;

                case CommandString.List:
                    HandleListTopic();
                    break;
                case CommandString.CreateTopic:
                    HandleCreateTopic(command.GetDeserializedData<TopicDto>());
                    break;
                case CommandString.Join:
                    HandleJoinTopic(command.GetDeserializedData<TopicDto>());
                    break;
                case CommandString.Error:
                    break;
                default: throw new NotImplementedException();
            }
        }
    }
}