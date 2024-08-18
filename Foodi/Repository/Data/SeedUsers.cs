using Microsoft.AspNetCore.Identity;
using Foodi.Core.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Foodi.Core.Consts;


namespace Foodi.Repository.Data
{
    public class SeedUsers
    {
        public static async Task AddAdmin(UserManager<User> userManager)
        {
            //create user (admin)
            var admin = new User
            {
                FirstName = "Fatma",
                LastName = "Ashraf",
                UserName = "Admin@foodi.com",
                Email ="Admin@foodi.com",
                EmailConfirmed = true,
                CreatedDate = DateTime.Now
            };
            var user = await userManager.FindByEmailAsync(admin.Email);

            if (user == null)
            {
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, AppRoles.Admin);
                //var result = await userManager.CreateAsync(admin, "Admin@123");
                //if (result.Succeeded)
                //{
                //    await userManager.AddToRoleAsync(admin,AppRoles.Admin);
                //    Console.WriteLine("Admin user created successfully.");
                //}
                //else
                //{
                //    Console.WriteLine($"Error creating admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                //}
            }
            
        }
    }
}
