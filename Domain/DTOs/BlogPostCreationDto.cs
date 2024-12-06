namespace Domain.DTOs;

public class BlogPostCreationDto
{
   
    public string Title { get; }
    public string Content { get; }
    public string Country { get; }

    public BlogPostCreationDto(string title, string content, string country)
    {
        Title = title;
        Content = content;
        Country = country;
    }
}