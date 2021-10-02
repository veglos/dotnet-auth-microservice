using System;

namespace Auth.Application.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException() { }
        public InvalidTokenException(string message) : base(message) { }
        public InvalidTokenException(string message, Exception innerException) : base(message, innerException) { }
    }
}
