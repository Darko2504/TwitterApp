using Microsoft.EntityFrameworkCore;
using TwitterApp.DataAcess.Repository.Abstractions;
using TwitterApp.DataAcess.TwitterAppDbContext;
using TwitterApp.Domain.Entities;

namespace TwitterApp.DataAcess.Repository.Implementations
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly TwitterAppDbContext.TwitterAppDbContext _db;
        public PostRepository(TwitterAppDbContext.TwitterAppDbContext db) : base(db) {
            _db = db;   
        }

        public async Task<List<Post>> GetFeedAsync()
        {
            return await _db.Posts
                .Include(p => p.User)
                .Include(p => p.Likes)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Post>> GetPostsByUserAsync(string userId)
        {
            return await _db.Posts
                .Where(p => p.UserId == userId)
                .Include(p => p.Likes)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}
