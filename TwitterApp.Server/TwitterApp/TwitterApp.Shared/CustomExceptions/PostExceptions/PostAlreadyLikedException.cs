namespace TwitterApp.Shared.CustomExceptions.PostExceptions
{
    public class PostAlreadyLikedException : Exception
    {
        public PostAlreadyLikedException(string message) : base(message) { }
    }
}
