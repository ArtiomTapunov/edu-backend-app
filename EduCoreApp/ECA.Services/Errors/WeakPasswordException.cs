using System;
using System.Runtime.Serialization;

namespace ECA.Services.Errors
{
    public class WeakPasswordException : Exception
    {
        public WeakPasswordException() { }

        public WeakPasswordException(string message) : base(message) { }

        public WeakPasswordException(string message, Exception innerException) : base(message, innerException) { }

        protected WeakPasswordException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
