using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IBlogPostLogic
{
    Task<BlogPost> CreateAsync(BlogPostCreationDto dto, string userId);
    Task<IEnumerable<BlogPost>> GetAsync(SearchBlogPostParametersDto searchParameters);
    Task UpdateAsync(BlogPostUpdateDto blogPost);
    Task DeleteAsync(int id);
    Task<BlogPostBasicDto> GetByIdAsync(int id);
}