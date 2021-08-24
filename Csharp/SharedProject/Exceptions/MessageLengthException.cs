using System;

namespace SharedProject.Exceptions
{
    class MessageLengthException : Exception
    {
        public MessageLengthException(int eccess)
            : base($"Message exceed 280 char limit with an overflow of {eccess}") { }
    }
}
