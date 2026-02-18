namespace CommunityApiV3.DTOs.BlogPosts
{
    public class BlogPostResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
