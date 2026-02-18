using CommunityApiV3.DTOs.BlogPosts;
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

        public BlogPostService(
            IBlogPostRepository blogPostRepository,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository)
        {
            _blogPostRepository = blogPostRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<BlogPostResponseDto>> GetAllAsync()
        {
            var posts = await _blogPostRepository.GetAllAsync();

            return posts.Select(p => new BlogPostResponseDto
            {
                Id = p.Id,
                Title = p.Title,
                Text = p.Text,
                Username = p.User.Username,
                CategoryName = p.Category.Name
            }).ToList();
        }

        public async Task<BlogPostResponseDto?> GetByIdAsync(int id)
        {
            var post = await _blogPostRepository.GetByIdAsync(id);
            if (post == null) return null;

            return new BlogPostResponseDto
            {
                Id = post.Id,
                Title = post.Title,
                Text = post.Text,
                Username = post.User.Username,
                CategoryName = post.Category.Name
            };
        }

        public async Task<List<BlogPostResponseDto>> GetByTitleAsync(string title)
        {
            var posts = await _blogPostRepository.GetByTitleAsync(title);

            return posts.Select(p => new BlogPostResponseDto
            {
                Id = p.Id,
                Title = p.Title,
                Text = p.Text,
                Username = p.User.Username,
                CategoryName = p.Category.Name
            }).ToList();
        }

        public async Task<List<BlogPostResponseDto>> GetByCategoryAsync(int categoryId)
        {
            var posts = await _blogPostRepository.GetByCategoryAsync(categoryId);

            return posts.Select(p => new BlogPostResponseDto
            {
                Id = p.Id,
                Title = p.Title,
                Text = p.Text,
                Username = p.User.Username,
                CategoryName = p.Category.Name
            }).ToList();
        }

        public async Task<int?> CreateAsync(CreateBlogPostDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null) 
                return null;


            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            if (category == null) 
                return null;


            var post = new BlogPost
            {
                Title = dto.Title,
                Text = dto.Text,
                UserId = dto.UserId,
                CategoryId = dto.CategoryId
            };

            await _blogPostRepository.AddAsync(post);
            return post.Id;
        }

        public async Task<bool> UpdateAsync(int id, UpdateBlogPostDto dto)
        {
            var post = await _blogPostRepository.GetByIdAsync(id);

            if (post == null) 
                return false;

            if (post.UserId != dto.UserId)
                return false;

            post.Title = dto.Title;
            post.Text = dto.Text;
            post.CategoryId = dto.CategoryId;

            await _blogPostRepository.UpdateAsync(post);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var post = await _blogPostRepository.GetByIdAsync(id);
            if (post == null) 
                return false;

            if (post.UserId != userId)
                return false;

            await _blogPostRepository.DeleteAsync(post);
            return true;
        }
    }
}
