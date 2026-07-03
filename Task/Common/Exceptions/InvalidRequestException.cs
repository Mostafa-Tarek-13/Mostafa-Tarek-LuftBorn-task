using System;

namespace Task.Common.Exceptions
{
    public sealed class InvalidRequestException : Exception
    {
        public InvalidRequestException(string message)
            : base(message)
        {
        }
    }
}
