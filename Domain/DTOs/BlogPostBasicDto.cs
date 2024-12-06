namespace Domain.DTOs;

public class BlogPostBasicDto
{
    public int Id { get; }
    public string AuthorName { get; }
    public string Title { get; }
    public string Content { get; }
    public string Country { get; }
    public bool IsPublished { get;  }

    public BlogPostBasicDto(int id, string authorName, string title, string content, string country, bool isPublished)
    {
        Id = id;
        AuthorName = authorName;
        Title = title;
        Content = content;
        Country = country;
        IsPublished = isPublished;
    }
}