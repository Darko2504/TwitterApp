using TwitterApp.Domain.Entities;

namespace TwitterApp.DataAcess.Repository.Abstractions
{
    public interface IPostLikeRepository : IRepository<PostLike>
    {
        Task<PostLike?> GetByPostAndUserAsync(int postId, string userId);
        Task<bool> ExistsAsync(int postId, string userId);
    }
}
