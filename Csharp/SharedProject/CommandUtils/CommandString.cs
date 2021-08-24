using System;

namespace SharedProject.CommandUtils
{
    [Flags]
    public enum CommandString
    {
        Register,
        Login,
        List,
        CreateTopic,
        Join,
        Send,
        DirectMessage,
        Error
    }
}