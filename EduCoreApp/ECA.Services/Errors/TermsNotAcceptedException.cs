using System;
using System.Runtime.Serialization;

namespace ECA.Services.Errors
{
    public class TermsNotAcceptedException : Exception
    {
        public TermsNotAcceptedException() { }

        public TermsNotAcceptedException(string message) : base(message) { }

        public TermsNotAcceptedException(string message, Exception innerException) : base(message, innerException) { }

        protected TermsNotAcceptedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
