using ElectronicDepartment.Common.Enums;
using ElectronicDepartment.DomainEntities;
using Microsoft.AspNetCore.Identity;
using static ElectronicDepartment.Common.Constants;

namespace ElectronicDepartment.BusinessLogic.Helpers
{
    public static class FirstInit
    {
        public static async Task InitRoles(RoleManager<IdentityRole> _roleManager, IEnumerable<string> roles)
        {
            foreach (var item in roles)
            {
                var res = await _roleManager.FindByNameAsync(item);

                if (res is null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(item));
                }
            }
        }

        public static async Task InitAdmin(UserManager<ApplicationUser> userManager)
        {
            var password = "admin123!";
            var admin = new Admin()
            {
                BirthDay = DateTime.Now,
                Email = @"admin123@gmail.com",
                FirstName = "Admin",
                MiddleName = "Admin",
                LastName = "Admin",
                Gender = Gender.None,
                UserName = @"adminAdmin",
                EmailConfirmed = true,
                UserType = UserType.Admin,
                PhoneNumber = "+380111111111",
                PhoneNumberConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(admin.Email);

            if (user is null)
            {
                var result = await userManager.CreateAsync(admin, password);

                var roles = await userManager.GetRolesAsync(admin);

                if(roles.Count() < 0)
                {
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, ADMINROLE);
                    }
                }
            }
        }
    }
}