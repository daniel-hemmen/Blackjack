using System.Runtime.Serialization;

namespace Blackjack
{
    [Serializable]
    public class ResultCannotBeShownException : Exception
    {
        public ResultCannotBeShownException()
        {
        }

        public ResultCannotBeShownException(string? message) : base(message)
        {
        }

        public ResultCannotBeShownException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ResultCannotBeShownException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}