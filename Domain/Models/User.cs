using System.Text.Json.Serialization;

namespace Domain.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public int SecurityLevel { get; set; }
    // public List<BlogPost> BlogPosts { get; set; } = new List<BlogPost>(); // User's blog posts
    // [JsonIgnore]
    // public ICollection<BlogPost> BlogPosts { get; set; }
}