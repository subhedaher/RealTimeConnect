using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealTimeConnect.Interfaces;
using RealTimeConnect.Models;
using RealTimeConnect.Settings;

namespace RealTimeConnect.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> userManager;
        private readonly JWTSetting jWTSetting;

        public AuthenticationService(IOptions<JWTSetting> jWTSetting, UserManager<User> userManager)
        {
            this.jWTSetting = jWTSetting.Value;
            this.userManager = userManager;
        }

        public async Task<string> GetJwtTokenAsync(User user)
        {
            var claims = new List<Claim>();

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName ?? user.FirstName));

            var token = new JwtSecurityToken(jWTSetting.Issuer, jWTSetting.Audience, claims,
                expires: DateTime.UtcNow.AddHours(jWTSetting.ExpirePerHour),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jWTSetting.Secret)),
                    SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return accessToken;
        }
    }
}
