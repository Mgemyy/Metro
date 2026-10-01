using MetroApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace MetroApp
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAndAdminAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminEmail = "admin@metro.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Cairo Metro Inspector",
                    NationalId = "29001011234567",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");

                    var profile = new EmployeeProfile
                    {
                        UserId = adminUser.Id,
                        EmployeeCode = "EMP-1001",
                        Department = "Subscriptions Dept",
                        OfficeLocation = "Shohadaa Station Office"
                    };

                    context.EmployeeProfiles.Add(profile);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
