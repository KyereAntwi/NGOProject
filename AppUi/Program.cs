using System;
using System.Threading.Tasks;
using AppDataAccess.Data;
using AppModels.DTO;
using AppUi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppUi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope()) 
            {
                var identityContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

                await identityContext.Database.MigrateAsync();
                await dbContext.Database.MigrateAsync();

                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var dataContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

                if (!await roleManager.RoleExistsAsync("Administrator"))
                {
                    var adminRole = new IdentityRole("Administrator");
                    await roleManager.CreateAsync(adminRole);
                }

                if (!await roleManager.RoleExistsAsync("Volunteer"))
                {
                    var adminRole = new IdentityRole("Volunteer");
                    await roleManager.CreateAsync(adminRole);
                }

                if (!await dataContext.ApplicationUsers.AnyAsync())
                {
                    var user = new IdentityUser
                    {
                        UserName = "master@email.com",
                        Email = "master@email.com",
                        PhoneNumber = "0000000000"
                    };
                    var response = await userManager.CreateAsync(user, "MasterPassword@1992");

                    if (response.Succeeded)
                    {
                        var newUser = await userManager.FindByEmailAsync(user.Email);
                        var roleResponse = await userManager.AddToRoleAsync(newUser, "Administrator");

                        if (roleResponse.Succeeded)
                        {
                            ApplicationUser applicationUser = new ApplicationUser
                            {
                                Name = "Master User",
                                Gender = "Male",
                                DateOfBirth = DateTime.Now.Date,
                                UserId = newUser.Id,
                                DateAdded = DateTime.Now.Date,
                                Nationality = "Ghanaian"
                            };

                            await dataContext.ApplicationUsers.AddAsync(applicationUser);
                            await dataContext.SaveChangesAsync();
                        }
                    }
                }

                await host.RunAsync();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
