using TwitterApp.Dtos;
using TwitterApp.Shared.Responses;

namespace TwitterApp.Services.Abstractions
{
    public interface IPostService
    {
        Task<CustomResponse<PostDto>> CreatePostAsync(CreatePostDto dto, string userId);
        Task<CustomResponse<List<PostDto>>> GetFeedAsync(string currentUserId);
        Task<CustomResponse<List<PostDto>>> GetUserPostsAsync(string userId, string currentUserId);
        Task<CustomResponse> LikePostAsync(int postId, string userId);
        Task<CustomResponse> UnlikePostAsync(int postId, string userId);
        Task<CustomResponse<PostDto>> RetweetPostAsync(int postId, string userId);

    }
}
