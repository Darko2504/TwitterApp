namespace TwitterApp.Dtos
{
    public class CreatePostDto
    {
        public string Content { get; set; }
        public int? RetweetOfPostId { get; set; }
    }
}
