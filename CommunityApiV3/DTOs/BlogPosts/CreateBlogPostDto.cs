namespace CommunityApiV3.DTOs.BlogPosts
{
    public class CreateBlogPostDto
    {
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}
