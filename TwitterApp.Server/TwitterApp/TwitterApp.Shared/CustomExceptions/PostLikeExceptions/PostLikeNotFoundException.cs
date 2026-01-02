namespace TwitterApp.Shared.CustomExceptions.PostLikeExceptions
{
    public class PostLikeNotFoundException : Exception
    {
        public PostLikeNotFoundException(string message) : base(message) { }
    }
}
