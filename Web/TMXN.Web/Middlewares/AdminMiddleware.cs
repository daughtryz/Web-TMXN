using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Common;
using TMXN.Data.Models;

namespace TMXN.Web.Middlewares
{
    public class AdminMiddleware
    {
        private readonly RequestDelegate next;

        public AdminMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            await this.SeedUserInRoles(userManager);
            await this.next(context);
        }

        private async Task SeedUserInRoles(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any(x => x.UserName == GlobalConstants.AdministratorUsername))
            {
                var user = new ApplicationUser
                {
                    UserName = GlobalConstants.AdministratorUsername,
                    Email = GlobalConstants.AdministratorEmail,
                    
                };

                var result = await userManager.CreateAsync(user, GlobalConstants.AdministratorPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                }
                else
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
