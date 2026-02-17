using CommunityApiV3.Models;

namespace CommunityApiV3.Services.Interfaces
{
    public interface ICommentService
    {
        Task<List<Comment>> GetByPostIdAsync(int postId);
        Task<string> CreateAsync(Comment comment);
    }
}
