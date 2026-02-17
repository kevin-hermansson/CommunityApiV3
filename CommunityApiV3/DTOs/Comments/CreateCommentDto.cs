namespace CommunityApiV3.DTOs.Comments
{
    public class CreateCommentDto
    {
        public string Text { get; set; }
        public int BlogPostId { get; set; }
    }
}
