// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ActivatorTest
{
    public class JsonSerializationBinderException : JsonSerializationException
    {
        public JsonSerializationBinderException() { }

        public JsonSerializationBinderException(string message) : base(message) { }

        public JsonSerializationBinderException(string message, Exception innerException) : base(message, innerException) { }

        public JsonSerializationBinderException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }


}
