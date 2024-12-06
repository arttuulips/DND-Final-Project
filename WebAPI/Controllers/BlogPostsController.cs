using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogPostsController : ControllerBase
{
    private readonly IBlogPostLogic blogPostLogic;

    public BlogPostsController(IBlogPostLogic blogPostLogic)
    {
        this.blogPostLogic = blogPostLogic;
    }

 [Authorize]
    [HttpPost]
    public async Task<ActionResult<BlogPost>> CreateAsync([FromBody] BlogPostCreationDto dto)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            BlogPost created = await blogPostLogic.CreateAsync(dto, userId);
            return Created($"/blogPosts/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BlogPost>>> GetAsync([FromQuery] string? userName, [FromQuery] int? userId,
        [FromQuery] bool? publishedStatus, [FromQuery] string? titleContains, [FromQuery] string? contentContains, [FromQuery] string? countryContains)
    {
        try
        {
            Console.WriteLine("Get post controller");
            SearchBlogPostParametersDto parameters = new(userName, userId, publishedStatus, titleContains, contentContains, countryContains);
            var blogPosts = await blogPostLogic.GetAsync(parameters);
            return Ok(blogPosts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] BlogPostUpdateDto dto)
    {
        try
        {
            await blogPostLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await blogPostLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BlogPostBasicDto>> GetById([FromRoute] int id)
    {
        try
        {
            BlogPostBasicDto result = await blogPostLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}