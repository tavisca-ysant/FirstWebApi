using System;
using System.Runtime.Serialization;

namespace FirstWebApi.Service
{
    [Serializable]
    public class InvalidBookParametersException : Exception
    {
        public InvalidBookParametersException()
        {
        }

        public InvalidBookParametersException(string message) : base(message)
        {
        }

        public InvalidBookParametersException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidBookParametersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}