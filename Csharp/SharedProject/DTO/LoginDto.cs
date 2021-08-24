namespace SharedProject.DTO
{
    public class LoginDto : CommandDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}