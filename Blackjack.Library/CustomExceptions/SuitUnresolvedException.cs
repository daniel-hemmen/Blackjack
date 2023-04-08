using System.Runtime.Serialization;

namespace Blackjack
{
    [Serializable]
    public class SuitUnresolvedException : Exception
    {
        public SuitUnresolvedException()
        {
        }

        public SuitUnresolvedException(string? message) : base(message)
        {
        }

        public SuitUnresolvedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SuitUnresolvedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}