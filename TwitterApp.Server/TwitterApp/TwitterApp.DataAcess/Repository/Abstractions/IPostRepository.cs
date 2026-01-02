using TwitterApp.Domain.Entities;

namespace TwitterApp.DataAcess.Repository.Abstractions
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<List<Post>> GetFeedAsync();
        Task<List<Post>> GetPostsByUserAsync(string userId);
    }
}
