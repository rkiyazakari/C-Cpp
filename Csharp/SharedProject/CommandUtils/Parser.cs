using System;
using SharedProject.Utils;
using SharedProject.Exceptions;

namespace SharedProject.CommandUtils
{
    public class Parser
    {
        public static string Sanitize(string message)
        {
            message = message.Trim();
            if (!Constants.RequestRegex.IsMatch(message)) throw new BadRequestException();
            return message;
        }

        public static string GetPrefix(string message)
        {
            return message.Split(':')[0];
        }
        
        public static string GetData(string message)
        {
            var index = message.IndexOf(':');
            return message[(index + 1)..];

        }
    }
}