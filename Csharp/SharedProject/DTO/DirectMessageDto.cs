namespace SharedProject.DTO
{
    public class DirectMessageDto : CommandDto
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Text { get; set; }
    }
}