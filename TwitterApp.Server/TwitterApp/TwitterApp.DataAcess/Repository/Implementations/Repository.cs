using Microsoft.EntityFrameworkCore;
using TwitterApp.DataAcess.Repository.Abstractions;
using TwitterApp.DataAcess.TwitterAppDbContext;

namespace TwitterApp.DataAcess.Repository.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TwitterAppDbContext.TwitterAppDbContext _db;

        public Repository(TwitterAppDbContext.TwitterAppDbContext db)
        {
            _db = db;
            
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _db.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
