using System;

namespace ContactsApi.Core.Exceptions
{
    public class NoResourceFoundException : Exception
    {
        public NoResourceFoundException(string message) : base(message) { }
    }
}
