namespace TwitterApp.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }
        public string Username { get; set; }

        public int LikesCount { get; set; }
        public bool IsRetweet { get; set; }
        public int? RetweetOfPostId { get; set; }
    }
}
