namespace CommunityApiV3.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get ; set; }  = string.Empty;
        public List<BlogPost> BlogPosts { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();


    }
}
