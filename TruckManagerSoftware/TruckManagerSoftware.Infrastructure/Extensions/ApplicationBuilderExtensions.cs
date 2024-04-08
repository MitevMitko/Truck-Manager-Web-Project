namespace TruckManagerSoftware.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using Data.Models;

    using static Common.DataConstants.DataConstants.Admin;
    using static Common.DataConstants.DataConstants.User;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedAdmin(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider services = scopedServices.ServiceProvider;

            UserManager<User> userManager = services.GetRequiredService<UserManager<User>>();

            RoleManager<IdentityRole<Guid>> roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdminRoleName))
                    {
                        return;
                    }

                    IdentityRole<Guid> role = new IdentityRole<Guid>(AdminRoleName);

                    await roleManager.CreateAsync(role);

                    User adminUser = await userManager.FindByEmailAsync(AdminDevelopmentEmail);

                    await userManager.AddToRoleAsync(adminUser, AdminRoleName);
                })
                .GetAwaiter()
                .GetResult();

            return app;
        }

        public static IApplicationBuilder SeedUser(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider services = scopedServices.ServiceProvider;

            UserManager<User> userManager = services.GetRequiredService<UserManager<User>>();

            RoleManager<IdentityRole<Guid>> roleManager = services.GetRequiredService < RoleManager<IdentityRole<Guid>>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(UserRoleName))
                    {
                        return;
                    }

                    IdentityRole<Guid> role = new IdentityRole<Guid>(UserRoleName);

                    await roleManager.CreateAsync(role);

                    User userUser = await userManager.FindByEmailAsync(UserDevelopmentEmail);

                    await userManager.AddToRoleAsync(userUser, UserRoleName);
                })
                .GetAwaiter()
                .GetResult();

            return app;
        }
    }
}
