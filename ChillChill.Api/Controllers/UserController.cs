using ChillChill.Api.Data;
using ChillChill.Contract.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ChillChill.Api.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<UserDTO>> GetUserProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var profile = await _context.Users.Where(user => user.Id == Guid.Parse(userId)).Select(user => new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                DisplayName = user.DisplayName
            }).FirstOrDefaultAsync();

            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }
    }
}
