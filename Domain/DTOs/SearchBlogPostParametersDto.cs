namespace Domain.DTOs;

public class SearchBlogPostParametersDto
{
    public string? UserName { get;}
    public int? UserId { get;}
    public bool? PublishedStatus { get;}
    public string? TitleContains { get;}
    public string? ContentContains { get;}
    public string? CountryContains { get;}

    public SearchBlogPostParametersDto(string? userName, int? userId, bool? publishedStatus, string? titleContains, string? contentContains, string? countryContains)
    {
        UserName = userName;
        UserId = userId;
        PublishedStatus = publishedStatus;
        TitleContains = titleContains;
        ContentContains = contentContains;
        CountryContains = countryContains;
    } 
}