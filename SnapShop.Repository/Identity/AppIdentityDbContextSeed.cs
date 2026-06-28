using Microsoft.AspNetCore.Identity;
using SnapShop.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapShop.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> user)
        {
            if (!user.Users.Any())
            {
                var newUser = new AppUser
                {
                    DisplayName = "AboElsho2",
                    Email = "shawky25557032@gmail.com",
                    UserName = "AboElsho2",
                    PhoneNumber = "01066739590",

                };

                await user.CreateAsync(newUser,"Password");
            }
        }
    }
}
