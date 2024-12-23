using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Data.SeedData
{
    public static class IdentitySeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // اضافه کردن نقش‌ها
            string[] roles = { "User", "Agent", "Lawyer" };  // افزودن "Lawyer" به لیست نقش‌ها

            // ایجاد نقش‌ها اگر وجود نداشته باشند
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // ایجاد یک کاربر نمونه با نقش "User"
            var user = new IdentityUser { UserName = "testuser", Email = "user@example.com" };
            if (userManager.Users.All(u => u.UserName != user.UserName))
            {
                await userManager.CreateAsync(user, "Password123!");
                await userManager.AddToRoleAsync(user, "User");
            }

            var user1 = new IdentityUser { UserName = "farshad", Email = "www.fadia8094@gmail.com" };
            if (userManager.Users.All(u => u.UserName != user1.UserName))
            {
                await userManager.CreateAsync(user1, "Password123!");
                await userManager.AddToRoleAsync(user1, "User");
            }

            // ایجاد یک کاربر نمونه با نقش "Agent"
            var agent = new IdentityUser { UserName = "testagent", Email = "agent@example.com" };
            if (userManager.Users.All(u => u.UserName != agent.UserName))
            {
                await userManager.CreateAsync(agent, "Password123!");
                await userManager.AddToRoleAsync(agent, "Agent");
            }

            // ایجاد یک کاربر نمونه با نقش "Lawyer"
            var lawyer = new IdentityUser { UserName = "testlawyer", Email = "lawyer@example.com" };
            if (userManager.Users.All(u => u.UserName != lawyer.UserName))
            {
                await userManager.CreateAsync(lawyer, "Password123!");
                await userManager.AddToRoleAsync(lawyer, "Lawyer");
            }
        }
    }
}
