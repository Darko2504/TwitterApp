namespace TwitterApp.Dtos
{
    public class UserProfileDto
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public List<PostDto> Posts { get; set; }
    }
}
