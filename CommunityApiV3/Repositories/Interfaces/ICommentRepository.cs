using CommunityApiV3.Models;

namespace CommunityApiV3.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetByPostIdAsync(int postId);
        Task AddAsync(Comment comment);
    }
}
