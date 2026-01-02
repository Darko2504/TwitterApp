namespace TwitterApp.Shared.CustomExceptions.PostExceptions
{
    public class PostCreateException : Exception
    {
        public PostCreateException(string message) : base(message) { }

    }
}
