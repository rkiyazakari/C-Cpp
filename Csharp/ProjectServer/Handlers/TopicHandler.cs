using ProjectServer.Models;
using ProjectServer.Services;
using Serilog;
using SharedProject;
using SharedProject.DTO;

namespace ProjectServer.Handlers
{
    internal partial class WebSocketHandler
    {
        private void HandleCreateTopic(TopicDto newTopic)
        {
            var topic = new Topic {Title = newTopic.Title, Description = newTopic.Description};
            if (TopicService.AddTopic(topic))
            {
                Log.Information($"Topic {topic.Title} created");
                Communication.SendSuccess(_webSocket);
            }
            else
            {
                Communication.SendError(_webSocket, "Wrong username or password");
            }
        }

        private void HandleJoinTopic(TopicDto topic)
        {
            if (!AuthService.IsLoggedIn(_user))
            {
                Communication.SendError(_webSocket, "You must be logged in to join topic");
                Log.Warning($"Not logged in user tried to join topic {topic.Title}");
                return;
            }


            if (TopicService.JoinTopic(topic.Title, _user.UserId))
            {
                Log.Information($"User {_user.Username} joined topic {topic.Title}");
                Communication.SendSuccess(_webSocket);
            }
            else
            {
                Log.Information($"User {_user.Username} could not join topic {topic.Title}");
                Communication.SendError(_webSocket, $"Could not join topic {topic.Title}");
            }
        }

        private void HandleListTopic()
        {
            var topicList = TopicService.GetTopics();

            if (topicList == null)
            {
                Communication.SendError(_webSocket, "Error while fetching data");
                return;
            }

            var topicListResponse = topicList.ConvertAll(topic => new TopicDto
            {
                Id = topic.TopicsId,
                Title = topic.Title,
                Description = topic.Description
            });

            Communication.SendListResponse(_webSocket, topicListResponse);
        }
    }
}