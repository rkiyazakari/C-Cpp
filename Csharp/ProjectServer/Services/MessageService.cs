using System.Linq;
using System.Net.WebSockets;
using ProjectServer.Models;
using Serilog;
using SharedProject;
using SharedProject.DTO;

namespace ProjectServer.Services
{
    public static class MessageService
    {
        public static bool SaveDirectMessage(DirectMessage newDirectMessage)
        {
            var context = DbServices.Instance.Context;

            try
            {
                context.DirectMessages.Add(newDirectMessage);
                context.SaveChanges();
                Log.Information(
                    $"User {newDirectMessage} sent direct message to user {newDirectMessage.Receiver}");
                return true;
            }
            catch
            {
                Log.Error("Could not save direct message");
                return false;
            }
        }

        public static void SendDirectMessage(DirectMessage directMessage, WebSocket receiverWebsocket)
        {
            var receiver = AuthService.GetUserFromId(directMessage.Receiver);
            var sender = AuthService.GetUserFromId(directMessage.Sender);
            var message = new DirectMessageDto
            {
                Sender = sender.Username,
                Receiver = receiver.Username,
                Text = directMessage.Text
            };

            Communication.SendResponse(receiverWebsocket, message);
        }

        public static TopicMessage SaveTopicMessage(TopicMessage newTopicMessage)
        {
            var context = DbServices.Instance.Context;

            try
            {
                context.TopicMessages.Add(newTopicMessage);
                context.SaveChanges();
                Log.Information(
                    $"User {newTopicMessage.UserId} sent direct message to user {newTopicMessage.Topics.Title}");
                return context.TopicMessages.FirstOrDefault(tm => tm == newTopicMessage);
            }
            catch
            {
                Log.Error("Could not save topic message");
                return null;
            }
        }

        public static void SendTopicMessage(TopicMessage topicMessage, WebSocket receiverWebsocket)
        {
            var message = new TopicMessageDto
            {
                TopicTitle = topicMessage.Topics.Title,
                Sender = topicMessage.User.Username,
                Text = topicMessage.Text,
                CreatedAt = topicMessage.CreatedAt.ToLocalTime().ToString()
            };

            Communication.SendResponse(receiverWebsocket, message);
        }
    }
}