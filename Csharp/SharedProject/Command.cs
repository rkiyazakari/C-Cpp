using System;
using System.Text;
using System.Text.Json;
using SharedProject.CommandUtils;
using SharedProject.DTO;
using SharedProject.Exceptions;

namespace SharedProject
{
    public class Command
    {
        public Command(string message)
        {
            try
            {
                message = Parser.Sanitize(message);
                Prefix = Parser.GetPrefix(message);
                Data = Parser.GetData(message);
            }
            catch (BadRequestException e)
            {
                Type = CommandString.Error;
                Data = $"{{\"message\": \"{e.Message}\"}}";
            }
        }

        public string Prefix
        {
            get
            {
                return Type switch
                {
                    CommandString.Register => CommandString.Register.ToString().ToLower(),
                    CommandString.Join => CommandString.Join.ToString().ToLower(),
                    CommandString.Login => CommandString.Login.ToString().ToLower(),
                    CommandString.Send => CommandString.Send.ToString().ToLower(),
                    CommandString.DirectMessage => CommandString.DirectMessage.ToString().ToLower(),
                    CommandString.List => CommandString.List.ToString().ToLower(),
                    CommandString.CreateTopic => CommandString.CreateTopic.ToString().ToLower(),
                    _ => "error"
                };
            }

            set
            {
                try
                {
                    Type = (CommandString) Enum.Parse(typeof(CommandString), value, true);
                }
                catch
                {
                    Console.WriteLine("Wrong prefix command, assigning Error type to the command");
                    Type = CommandString.Error;
                }
            }
        }

        public CommandString Type { get; private set; }

        public string Data { get; }

        public T GetDeserializedData<T>() where T : CommandDto
        {
            return JsonSerializer.Deserialize<T>(Data);
        }

        public byte[] ToByte()
        {
            return Encoding.UTF8.GetBytes(ToString());
        }

        public override string ToString()
        {
            return $"{Prefix}:{Data}";
        }
    }
}