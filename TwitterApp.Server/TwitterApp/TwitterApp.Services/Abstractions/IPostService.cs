using TwitterApp.Dtos;

namespace TwitterApp.Services.Abstractions
{
    public interface IPostService
    {
        Task<PostDto> CreatePostAsync(CreatePostDto dto, string userId);
        Task<List<PostDto>> GetFeedAsync();
        Task<List<PostDto>> GetUserPostsAsync(string userId);
        Task LikePostAsync(int postId, string userId);
        Task UnlikePostAsync(int postId, string userId);
    }
}
