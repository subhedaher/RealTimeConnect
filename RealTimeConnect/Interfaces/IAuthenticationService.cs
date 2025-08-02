using RealTimeConnect.Models;

namespace RealTimeConnect.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> GetJwtTokenAsync(User user);
    }
}
