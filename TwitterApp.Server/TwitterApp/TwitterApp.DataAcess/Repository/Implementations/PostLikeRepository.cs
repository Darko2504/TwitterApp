using Microsoft.EntityFrameworkCore;
using TwitterApp.DataAcess.Repository.Abstractions;
using TwitterApp.Domain.Entities;

namespace TwitterApp.DataAcess.Repository.Implementations
{
    public class PostLikeRepository : Repository<PostLike>, IPostLikeRepository
    {
        private readonly TwitterAppDbContext.TwitterAppDbContext _db;

        public PostLikeRepository(TwitterAppDbContext.TwitterAppDbContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<bool> ExistsAsync(int postId, string userId)
        {
            return await _db.PostLikes
                .AnyAsync(x => x.PostId == postId && x.UserId == userId);
        }

        public async Task<PostLike?> GetByPostAndUserAsync(int postId, string userId)
        {
            return await _db.PostLikes
                .FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);
        }
    }
}
