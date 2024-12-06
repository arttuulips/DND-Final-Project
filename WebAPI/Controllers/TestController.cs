using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TestController : ControllerBase
{
    /*[HttpGet("authorized")]
    public ActionResult GetAsAuthorized()
    {
        // Check if the user is logged in
        if (!User.Identity?.IsAuthenticated ?? true)
        {
            return Unauthorized("You are not logged in");
        }

        // Check if the user has a role claim
        Claim? roleClaim = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Role));
        if (roleClaim == null)
        {
            return StatusCode(403, "You have no role");
        }

        // Check if the user is an administrator
        if (!roleClaim.Value.Equals("Administrator", StringComparison.OrdinalIgnoreCase))
        {
            return StatusCode(403, "You are not an administrator");
        }

        // If all checks pass, return success
        return Ok("You are authorized and an administrator. You may proceed.");
    }*/
    
    [HttpGet("allowanon"), AllowAnonymous]
    public ActionResult GetAsAnon()
    {
        return Ok("This was accepted as anonymous");
    }

    // role
    [HttpGet("authorized")]
    public ActionResult GetAsAuthorized()
    {
        return Ok("This was accepted as authorized");
    }
    
    [HttpGet("manualcheck")]
    public ActionResult GetWithManualCheck()
    {
        Claim? claim = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Role));
        if (claim == null)
        {
            return StatusCode(403, "You have no role");
        }

        if (!claim.Value.Equals("Administrator"))
        {
            return StatusCode(403, "You are not an administrator");
        }

        return Ok("You are an administrator, you may proceed");
    }

}