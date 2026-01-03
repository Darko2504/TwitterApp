using TwitterApp.Dtos;
using TwitterApp.Shared.Responses;

namespace TwitterApp.Services.Abstractions
{
    public interface IPostService
    {
        Task<CustomResponse<PostDto>> CreatePostAsync(CreatePostDto dto, string userId);
        Task<CustomResponse<List<PostDto>>> GetFeedAsync();
        Task<CustomResponse<List<PostDto>>> GetUserPostsAsync(string userId);
        Task<CustomResponse> LikePostAsync(int postId, string userId);
        Task<CustomResponse> UnlikePostAsync(int postId, string userId);
    }
}
