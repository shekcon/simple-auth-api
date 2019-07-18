using System.Linq;
using Auth.API.Services;
using Auth.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Presistence
{
    public static class SeedData
    {
        public static void Initialize(DataContext context, IUserService userservice)
        {
            // context.
            context.Database.EnsureCreated();
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }
            userservice.Create(new User { username = "shekcon", email = "shekcon@gmail.com", role=1 }, "123456789");
        }
    }
}