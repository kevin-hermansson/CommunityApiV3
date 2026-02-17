using CommunityApiV3.Models;
using CommunityApiV3.Repositories.Interfaces;
using CommunityApiV3.Services.Interfaces;

namespace CommunityApiV3.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        
        public BlogPostService(IBlogPostRepository blogPostRepository,
            IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _blogPostRepository = blogPostRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<BlogPost>> GetAllAsync()
        {
            return await _blogPostRepository.GetAllAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(int id)
        {
            return await _blogPostRepository.GetByIdAsync(id);
        }

        public async Task<List<BlogPost>> SearchByTitleAsync(string title)
        {
            return await _blogPostRepository.GetByTitleAsync(title);
        }

        public async Task<List<BlogPost>> GetByCategoryAsync(int categoryId)
        {
            return await _blogPostRepository.GetByCategoryAsync(categoryId);
        }

        public async Task<bool> CreateAsync(BlogPost post)
        {
            var user = await _userRepository.GetByIdAsync(post.UserId);
            var category = await _categoryRepository.GetByIdAsync(post.CategoryId);

            if (user == null || category == null)
                return false;


            await _blogPostRepository.AddAsync(post);
            return true;
        }
        
        public async Task<string> UpdateAsync(int id, int userId, BlogPost updatedPost)
        {
            var existingPost = await _blogPostRepository.GetByIdAsync(id);

            if (existingPost == null)
                return "NotFound";

            if (existingPost.UserId != userId)
                return "Forbid";

            existingPost.Title = updatedPost.Title;
            existingPost.Text = updatedPost.Text;
            existingPost.CategoryId = updatedPost.CategoryId;

            await _blogPostRepository.UpdateAsync(existingPost);
            return "Success";
        }

        public async Task<string> DeleteAsync(int id, int userId)
        {
            var existingPost = await _blogPostRepository.GetByIdAsync(id);

            if (existingPost == null)
                return "NotFound";

            if (existingPost.UserId != userId)
                return "Forbid";

            await _blogPostRepository.DeleteAsync(existingPost);

            return "Success";
        }
    }
}
