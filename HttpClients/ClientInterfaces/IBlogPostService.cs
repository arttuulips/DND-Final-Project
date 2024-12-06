using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IBlogPostService
{
    Task CreateAsync(BlogPostCreationDto dto);
    Task<ICollection<BlogPost>> GetAsync(string? userName, int? userId, bool? publishedStatus, string? titleContains, string? contentContains, string? countryContains);
    Task UpdateAsync(BlogPostUpdateDto dto);
    Task<BlogPostBasicDto> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}