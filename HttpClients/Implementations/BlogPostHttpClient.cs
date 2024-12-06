using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BlazorWASM.Services;
using BlazorWASM.Services.Http;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class BlogPostHttpClient : IBlogPostService
{
    private readonly HttpClient client;

    public BlogPostHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(BlogPostCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/blogPosts",dto);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
    
    public async Task<ICollection<BlogPost>> GetAsync(string? userName, int? userId, bool? publishedStatus, string? titleContains, string? contentContains, string? countryContains)
    {
        string query = ConstructQuery(userName, userId, publishedStatus, titleContains, contentContains, countryContains);

        HttpResponseMessage response = await client.GetAsync("/blogPosts"+query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<BlogPost> blogPosts = JsonSerializer.Deserialize<ICollection<BlogPost>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return blogPosts;
    }
    private static string ConstructQuery(string? userName, int? userId, bool? publishedStatus, string? titleContains, string? contentContains, string? countryContains)
    {
        string query = "";
        if (!string.IsNullOrEmpty(userName))
        {
            query += $"?username={userName}";
        }

        if (userId != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"userid={userId}";
        }

        if (publishedStatus != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"publishedstatus={publishedStatus}";
        }

        if (!string.IsNullOrEmpty(titleContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"titlecontains={titleContains}";
        }

        if (!string.IsNullOrEmpty(contentContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"contentcontains={contentContains}";
        }

        if (!string.IsNullOrEmpty(countryContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"countrycontains={countryContains}";
        }

        return query;
    }
    
    public async Task UpdateAsync(BlogPostUpdateDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/blogPosts", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
    
    public async Task<BlogPostBasicDto> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/blogPosts/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        BlogPostBasicDto blogPost = JsonSerializer.Deserialize<BlogPostBasicDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return blogPost;
    }
    
    public async Task DeleteAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"BlogPosts/{id}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}