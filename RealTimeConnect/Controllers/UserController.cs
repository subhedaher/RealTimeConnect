using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealTimeConnect.DTOs;
using RealTimeConnect.Models;

namespace RealTimeConnect.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        public UserController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = await userManager.Users
                .Where(u => u.Id != int.Parse(currentUser))
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                })
                .ToListAsync();

            return Ok(users);
        }
    }
}
