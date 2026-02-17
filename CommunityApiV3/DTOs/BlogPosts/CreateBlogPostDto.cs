namespace CommunityApiV3.DTOs.BlogPosts
{
    public class CreateBlogPostDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
    }
}
