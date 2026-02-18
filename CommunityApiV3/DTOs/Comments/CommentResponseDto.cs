namespace CommunityApiV3.DTOs.Comments
{
    public class CommentResponseDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
