using Microsoft.AspNetCore.Identity;
using OBiletClone.Server.Domain.Entities;

namespace OBiletClone.Server.WebAPI.Middlewares;

public static class ExtensionsMiddleware
{
    public static void CreateFirstUser(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!userManager.Users.Any(p => p.UserName == "admin"))
            {
                var user = new AppUser()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    FirstName = "Cuma",
                    LastName = "Köse",
                    EmailConfirmed = true
                };

                userManager.CreateAsync(user, "1").Wait();
            }
        }
    }
}
