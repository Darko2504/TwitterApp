namespace TwitterApp.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // User reference
        public string UserId { get; set; }
        public User User { get; set; }

        // Retweet reference
        public int? RetweetOfPostId { get; set; }
        public Post RetweetOfPost { get; set; }

        // Likes
        public List<PostLike> Likes { get; set; } = new List<PostLike>();
    }
}
