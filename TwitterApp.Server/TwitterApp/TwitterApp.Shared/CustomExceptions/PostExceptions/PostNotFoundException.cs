namespace TwitterApp.Shared.CustomExceptions.PostExceptions
{
    public class PostNotFoundException : Exception
    {
        public PostNotFoundException(string message) : base(message)
        {
            
        }
    }
}
