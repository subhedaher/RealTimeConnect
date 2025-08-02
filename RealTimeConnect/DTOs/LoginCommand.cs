namespace RealTimeConnect.DTOs
{
    public class LoginCommand
    {
        public string UsernameOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
