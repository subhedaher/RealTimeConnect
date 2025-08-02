using Microsoft.AspNetCore.Identity;
using RealTimeConnect.Models;

namespace RealTimeConnect.Seeders
{
    public static class UserSeeder
    {
        public static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var random = new Random();

                for (int i = 1; i <= 5; i++)
                {
                    var user = new User
                    {
                        FirstName = $"User {i}",
                        LastName = $"User {i}",
                        UserName = $"UserName_{i}",
                        Email = $"Email {i}",
                        PhoneNumber = $"{random.Next(100000000, 999999999)}",
                    };

                    await userManager.CreateAsync(user, "PassW0rd!");
                }
            }
        }
    }
}
