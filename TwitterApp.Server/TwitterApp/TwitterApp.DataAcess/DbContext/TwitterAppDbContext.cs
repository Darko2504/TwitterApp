using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TwitterApp.Domain.Entities;

namespace TwitterApp.DataAcess.TwitterAppDbContext
{
    public class TwitterAppDbContext : IdentityDbContext<User>
    {
        public TwitterAppDbContext(DbContextOptions<TwitterAppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 

            #region Relationships

            builder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PostLike>()
                .HasOne(pl => pl.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(pl => pl.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PostLike>()
                .HasOne(pl => pl.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(pl => pl.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PostLike>()
                .HasKey(pl => new { pl.PostId, pl.UserId });

            builder.Entity<Post>()
                .HasOne(p => p.RetweetOfPost)
                .WithMany()
                .HasForeignKey(p => p.RetweetOfPostId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }
}
