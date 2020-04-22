using System;
using System.Runtime.Serialization;

namespace ExtensionMethods.Exceptions
{
    [Serializable]
    internal class NotNullableException : Exception
    {
        public NotNullableException()
        {
        }

        public NotNullableException(string message) : base(message)
        {
        }

        public NotNullableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotNullableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}