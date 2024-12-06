namespace Domain.Models;

public class BlogPost
{
    public int Id { get; set; }
    public User Author { get; private set; } // Links to the User who created the blog post
    public int AuthorId { get; set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public string Country { get; private set; }
    public DateTime CreatedAt { get; set; } 
    public bool IsPublished { get; set; }


    public BlogPost(int authorId, string title, string content, string country)
    {
        AuthorId = authorId;
        Title = title;
        Content = content;
        Country = country;
        CreatedAt = DateTime.UtcNow;
        IsPublished = false; // Defaults to false initially
    }
    
    private BlogPost() {}
}
