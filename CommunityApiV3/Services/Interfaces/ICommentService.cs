using CommunityApiV3.DTOs.Comments;
using CommunityApiV3.Models;

namespace CommunityApiV3.Services.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentResponseDto>> GetByPostIdAsync(int postId);

        Task<string> CreateAsync(CreateCommentDto dto);
    }
}
