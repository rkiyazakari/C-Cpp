using System;
using System.Text.Json;

namespace SharedProject.DTO
{
    [Serializable]
    public abstract class CommandDto
    {
        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}