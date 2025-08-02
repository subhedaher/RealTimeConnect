using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealTimeConnect.DTOs;
using RealTimeConnect.Interfaces;
using RealTimeConnect.Models;

namespace RealTimeConnect.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly UserManager<User> userManager;
        public AuthController(IAuthenticationService authenticationService, UserManager<User> userManager)
        {
            this.authenticationService = authenticationService;
            this.userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            if (loginCommand == null || string.IsNullOrEmpty(loginCommand.UsernameOrEmail) || string.IsNullOrEmpty(loginCommand.Password))
                return BadRequest("Invalid credentials.");


            var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginCommand.UsernameOrEmail || u.Email == loginCommand.UsernameOrEmail);


            if (user == null) return Unauthorized("User not found.");

            var result = await userManager.CheckPasswordAsync(user, loginCommand.Password);
            if (!result) return Unauthorized("Invalid password.");

            var token = await authenticationService.GetJwtTokenAsync(user);

            return Ok(new LoginResponseDto
            {
                User = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                },
                Token = token
            });
        }
    }
}
