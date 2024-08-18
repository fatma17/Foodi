using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Foodi.Core.Consts;
using System.Data;

namespace Foodi.Repository.Data
{
    public class SeedRoles()
    {
        public static async Task AddRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole<int>(AppRoles.Admin));
                await roleManager.CreateAsync(new IdentityRole<int>(AppRoles.User));
            }
        }
    }
}
