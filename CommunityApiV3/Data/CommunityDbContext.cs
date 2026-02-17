using CommunityApiV3.Models;
using Microsoft.EntityFrameworkCore;
namespace CommunityApiV3.Data
{
    public class CommunityDbContext : DbContext
    {
        public CommunityDbContext(DbContextOptions<CommunityDbContext> options)
            : base(options) 
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<BlogPost> Blogposts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
