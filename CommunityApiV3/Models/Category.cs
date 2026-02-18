namespace CommunityApiV3.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<BlogPost> BlogPosts { get; set; } = new();

    }
}
