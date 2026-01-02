namespace TwitterApp.Shared.CustomExceptions.UserExceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException(string message) : base(message) { }
    }
}
