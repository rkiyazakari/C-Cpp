using System;

namespace SharedProject.DTO
{
    [Serializable]
    public class InfoDto : CommandDto
    {
        public InfoDto(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}