using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollComputation.Data
{
    public class Seeder
    {
        public static async Task UserAndRoleSeedAsync(UserManager<IdentityUser> userManager, 
                                             RoleManager<IdentityRole> roleManager)
        {//create the roles
            string[] roles = { "Admin", "Manager", "Staff" };
            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists) 
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //Create Admin
            if(userManager.FindByEmailAsync("admin@yahoo.com").Result == null) 
            {
                IdentityUser user = new IdentityUser 
                {
                    UserName = "admin@yahoo.com",
                    Email = "admin@yahoo.com",
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Password@1").Result;
                if (identityResult.Succeeded) 
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }

            }

            //Create Manager
            if (userManager.FindByEmailAsync("manager@yahoo.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "manager@yahoo.com",
                    Email = "manager@yahoo.com",
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Password@1").Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                }

            }

            //Create staff user
            if (userManager.FindByEmailAsync("jane.doe@yahoo.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "jane.doe@yahoo.com",
                    Email = "jane.doe@yahoo.com",
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Password@1").Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Staff").Wait();
                }

            }

            //Create No role User
            //Create staff user
            if (userManager.FindByEmailAsync("john.doe@yahoo.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "john.doe@yahoo.com",
                    Email = "john.doe@yahoo.com",
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Password@1").Result;
                //No role assigned

            }
        }
        
    }
}
