namespace CommunityApiV3.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int BlogPostId { get; set; }
    }
}
