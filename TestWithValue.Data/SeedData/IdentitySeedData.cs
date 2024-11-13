using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Data.SeedData
{
    public static class  IdentitySeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "User", "Agent" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // ایجاد یک کاربر نمونه
            var user = new IdentityUser { UserName = "testuser", Email = "user@example.com" };
            await userManager.CreateAsync(user, "Password123!");
            await userManager.AddToRoleAsync(user, "User");

            var user1 = new IdentityUser { UserName = "farshad", Email = "www.fadia8094@gmail.com" };
            await userManager.CreateAsync(user1, "Password123!");
            await userManager.AddToRoleAsync(user1, "User");

            var agent = new IdentityUser { UserName = "testagent", Email = "agent@example.com" };
            await userManager.CreateAsync(agent, "Password123!");
            await userManager.AddToRoleAsync(agent, "Agent");
        }
    }
}
