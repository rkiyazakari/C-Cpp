using System;
using System.Linq;
using ProjectServer.Models;
using ProjectServer.Services;
using Serilog;
using SharedProject;
using SharedProject.DTO;

namespace ProjectServer.Handlers
{
    internal partial class WebSocketHandler
    {
        private void HandleDirectMessage(DirectMessageDto directMessageDto)
        {
            if (!AuthService.IsLoggedIn(_user))
            {
                Communication.SendError(_webSocket, "You must be logged in to send direct message");
                Log.Warning("Not logged in user tried to send direct message");
                return;
            }

            var receiver = AuthService.GetUserFromUsername(directMessageDto.Receiver);
            var directMessage = new DirectMessage
            {
                CreatedAt = DateTimeOffset.Now,
                Sender = _user.UserId,
                Receiver = receiver.UserId,
                Text = directMessageDto.Text
            };

            // Tries to save the direct message in the database
            if (MessageService.SaveDirectMessage(directMessage))
            {
                // Check if the user is currently connected to the server
                if (_connectedClient.ContainsKey(receiver))
                {
                    MessageService.SendDirectMessage(
                        directMessage,
                        _connectedClient[receiver]
                    );
                }

                Communication.SendSuccess(_webSocket);
            }
        }

        private void HandleTopicMessage(TopicMessageDto topicMessageDto)
        {
            if (!AuthService.IsLoggedIn(_user))
            {
                Communication.SendError(_webSocket, "You must be logged in to send direct message");
                Log.Warning("Not logged in user tried to send topic message");
                return;
            }

            var topic = TopicService.GetTopics(topicMessageDto.TopicTitle);
            if (topic != null)
            {
                var newTopicMessage = new TopicMessage
                {
                    CreatedAt = DateTimeOffset.Now,
                    Text = topicMessageDto.Text,
                    UserId = _user.UserId,
                    TopicsId = topic.TopicsId
                };

                var topicMessage = MessageService.SaveTopicMessage(newTopicMessage);
                if (topicMessage != null)
                {
                    var userList = TopicService.GetUserForTopic(topic);
                    foreach (var user in userList.Where(user => _connectedClient.ContainsKey(user)))
                    {
                        MessageService.SendTopicMessage(
                            topicMessage,
                            _connectedClient[user]
                        );
                    }
                }
                else
                {
                    Communication.SendError(_webSocket,
                        $"Could not save message to topic {topicMessageDto.TopicTitle}");
                    Log.Error("Error while saving topic message");
                }
            }
            else
            {
                Communication.SendError(_webSocket, $"Could not send message to topic {topicMessageDto.TopicTitle}");
                Log.Warning($"Topic {topicMessageDto.TopicTitle} do not exist");
            }
        }
    }
}