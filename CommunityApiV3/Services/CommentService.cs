using CommunityApiV3.Data.Repositories.Interfaces;
using CommunityApiV3.DTOs.Comments;
using CommunityApiV3.Models;
using CommunityApiV3.Services.Interfaces;

namespace CommunityApiV3.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IUserRepository _userRepository;

        public CommentService(ICommentRepository commentRepository, IBlogPostRepository blogPostRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _blogPostRepository = blogPostRepository;
            _userRepository = userRepository;
        }

        public async Task<List<CommentResponseDto>> GetByPostIdAsync(int postId)
        {
            var comments = await _commentRepository.GetByBlogPostIdAsync(postId);

            return comments.Select(c => new CommentResponseDto
            {
                Id = c.Id,
                Text = c.Text,
                Username = c.User!.Username
            }).ToList();
        }


        public async Task<string> CreateAsync(CreateCommentDto dto)
        {
            var post = await _blogPostRepository.GetByIdAsync(dto.BlogPostId);
            var user = await _userRepository.GetByIdAsync(dto.UserId);

            if (post == null || user == null)
                return "NotFound";

            if (post.UserId == dto.UserId)
                return "Forbid";

            var comment = new Comment
            {
                Text = dto.Text,
                UserId = dto.UserId,
                BlogPostId = dto.BlogPostId
            };

            await _commentRepository.AddAsync(comment);

            return "Success";
        }

    }
}
