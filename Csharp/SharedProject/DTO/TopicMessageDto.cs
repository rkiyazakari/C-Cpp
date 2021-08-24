namespace SharedProject.DTO
{
    public class TopicMessageDto : CommandDto
    {
        public string TopicTitle { get; set; }
        public string Text { get; set; }
        public string Sender { get; set; }
        public string CreatedAt { get; set; }
    }
}