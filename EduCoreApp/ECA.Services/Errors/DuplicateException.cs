﻿using System;
using System.Runtime.Serialization;

namespace ECA.Services.Errors
{
    public class DuplicateException :Exception
    {
        public DuplicateException() { }

        public DuplicateException(string message) : base(message) { }

        public DuplicateException(string message, Exception innerException) : base(message, innerException) { }

        protected DuplicateException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
