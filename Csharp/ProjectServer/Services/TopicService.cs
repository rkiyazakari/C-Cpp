using System.Collections.Generic;
using System.Linq;
using ProjectServer.Models;
using Serilog;

namespace ProjectServer.Services
{
    public class TopicService
    {
        /// <summary>
        ///     Get all the topics available
        /// </summary>
        /// <returns>A list of Topic object</returns>
        public static List<Topic> GetTopics()
        {
            var context = DbServices.Instance.Context;
            return context.Topics.ToList();
        }

        public static Topic GetTopics(string name)
        {
            var context = DbServices.Instance.Context;
            try
            {
                return context.Topics.Where(topic => topic.Title.Contains(name)).ToList()[0];
            }
            catch
            {
                return null;
            }
        }

        public static List<Topic> GetConnectedTopics(int userId)
        {
            var context = DbServices.Instance.Context;

            return (from t in context.Topics
                    join c in context.Connects
                        on t.TopicsId equals c.TopicsId
                    where c.UserId == userId
                    select t
                ).ToList();
        }

        public static bool AddTopic(Topic newTopic)
        {
            var context = DbServices.Instance.Context;

            try
            {
                context.Topics.Add(newTopic);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool JoinTopic(string topicName, int userId)
        {
            var topicToJoin = GetTopics(topicName);

            if (topicToJoin == null)
            {
                Log.Error($"The topic {topicName} do not exist and can not be joined");
                return false;
            }

            var context = DbServices.Instance.Context;

            var newConnection = new Connect
            {
                UserId = userId,
                TopicsId = topicToJoin.TopicsId
            };
            context.Connects.Add(newConnection);
            context.SaveChanges();
            return true;
        }

        public static List<User> GetUserForTopic(Topic topic)
        {
            var context = DbServices.Instance.Context;

            return (from u in context.Users
                    join c in context.Connects
                        on u.UserId equals c.UserId
                    where c.TopicsId == topic.TopicsId
                    select u
                ).ToList();
        }
    }
}