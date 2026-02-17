namespace CommunityApiV3.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
