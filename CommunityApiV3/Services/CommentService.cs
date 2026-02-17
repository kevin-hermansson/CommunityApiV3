using CommunityApiV3.Models;
using CommunityApiV3.Repositories.Interfaces;
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

        public async Task<List<Comment>> GetByPostIdAsync(int postId)
        {
            return await _commentRepository.GetByPostIdAsync(postId);
        }

        public async Task<string> CreateAsync(Comment comment)
        {
            var post = await _blogPostRepository.GetByIdAsync(comment.BlogPostId);
            var user = await _userRepository.GetByIdAsync(comment.UserId);

            if (post == null || post == null)
                return "NotFound";

            if (post.UserId == comment.UserId)
                return "Forbid";

            await _commentRepository.AddAsync(comment);

            return "Success";

        }
    }
}
