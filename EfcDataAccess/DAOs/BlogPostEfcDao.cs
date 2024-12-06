using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class BlogPostEfcDao : IBlogPostDao
{
    public async Task<BlogPost> CreateAsync(BlogPost blogPost)
    {
        EntityEntry<BlogPost> added = await context.BlogPosts.AddAsync(blogPost);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<BlogPost>> GetAsync(SearchBlogPostParametersDto searchParameters)
    {
        IQueryable<BlogPost> query = context.BlogPosts.Include(blogPost => blogPost.Author).AsQueryable();
    
        if (!string.IsNullOrEmpty(searchParameters.UserName))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(blogPost =>
                blogPost.Author.UserName.ToLower().Equals(searchParameters.UserName.ToLower()));
        }
    
        if (searchParameters.UserId != null)
        {
            query = query.Where(t => t.Author.Id == searchParameters.UserId);
        }
        
        if (searchParameters.UserId != null)
        {
            query = query.Where(t => t.Author.Id == searchParameters.UserId);
        }
        
        if (!string.IsNullOrEmpty(searchParameters.ContentContains))
        {
            query = query.Where(t =>
                t.Content.ToLower().Contains(searchParameters.ContentContains.ToLower()));
        }
    
        if (!string.IsNullOrEmpty(searchParameters.CountryContains))
        {
            query = query.Where(t =>
                t.Country.ToLower().Contains(searchParameters.CountryContains.ToLower()));
        }
    
        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            query = query.Where(t =>
                t.Title.ToLower().Contains(searchParameters.TitleContains.ToLower()));
        }

        List<BlogPost> result = await query.ToListAsync();
        return result;
    }

    public async Task UpdateAsync(BlogPost blogPost)
    {
        context.BlogPosts.Update(blogPost);
        await context.SaveChangesAsync();
    }

    public async Task<BlogPost?> GetByIdAsync(int blogPostId)
    {
        BlogPost? found = await context.BlogPosts
            .Include(blogPost => blogPost.Author)
            .SingleOrDefaultAsync(blogPost => blogPost.Id == blogPostId);
        return found;
    }

    public async Task DeleteAsync(int id)
    {
        BlogPost? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Todo with id {id} not found");
        }

        context.BlogPosts.Remove(existing);
        await context.SaveChangesAsync();
    }
    
    private readonly BlogPostContext context;

    public BlogPostEfcDao(BlogPostContext context)
    {
        this.context = context;
    }
}