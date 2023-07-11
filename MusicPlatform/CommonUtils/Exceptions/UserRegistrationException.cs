using System.Runtime.Serialization;

namespace MusicPlatform.CommonUtils.Exceptions
{
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException()
        {
        }

        public UserRegistrationException(string? message) : base(message)
        {
        }

        public UserRegistrationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserRegistrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
