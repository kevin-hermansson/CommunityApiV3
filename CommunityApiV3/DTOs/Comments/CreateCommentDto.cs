namespace CommunityApiV3.DTOs.Comments
{
    public class CreateCommentDto
    {
        public int UserId { get; set; }

        public int BlogPostId { get; set; }

        public string Text { get; set; } = string.Empty;
    }
}
