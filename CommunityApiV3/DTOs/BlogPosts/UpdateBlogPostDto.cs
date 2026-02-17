namespace CommunityApiV3.DTOs.BlogPosts
{
    public class UpdateBlogPostDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
    }
}
