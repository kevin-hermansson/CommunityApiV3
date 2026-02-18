using CommunityApiV3.Models;

namespace CommunityApiV3.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetByBlogPostIdAsync(int blogPostId);
        Task AddAsync(Comment comment);
    }
}
