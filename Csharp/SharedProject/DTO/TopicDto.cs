using System;

namespace SharedProject.DTO
{
    [Serializable]
    public class TopicDto : CommandDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}