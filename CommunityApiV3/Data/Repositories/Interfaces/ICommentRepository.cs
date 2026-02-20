using CommunityApiV3.Models;

namespace CommunityApiV3.Data.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetByBlogPostIdAsync(int blogPostId);
        Task AddAsync(Comment comment);
    }
}
