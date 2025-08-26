namespace RalliesUK.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message, string userId)
            : base(message)
        {
            UserId = userId;
        }

        public UserNotFoundException(string message, Exception exception, string userId)
            : base(message, exception)
        {
            UserId = userId;
        }

        public string UserId { get; init; }

        public override string ToString()
        {
            return $"{base.ToString()}, UserId: {UserId}";
        }
    }
}