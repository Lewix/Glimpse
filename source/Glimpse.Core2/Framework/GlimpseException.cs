using System;
using System.Runtime.Serialization;

namespace Glimpse.Core2.Framework
{
    public class GlimpseException:Exception
    {
        public GlimpseException()
        {
        }

        public GlimpseException(string message) : base(message)
        {
        }

        public GlimpseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public GlimpseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}