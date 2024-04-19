using dataa.context;
using dataa.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repos;

namespace StoreApi.Helper
{
    public class ApplySeed
    {
        public static async Task applyseedingasync(WebApplication app)
        {

           using(var scope = app.Services.CreateScope())
            { 
            var services=scope.ServiceProvider;

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<storecontext>();
                    var usermanager = services.GetRequiredService<UserManager<AppUser>>();
                    await context.Database.MigrateAsync();
                    await seed.seedasync(context, loggerFactory);
                    await AppIdentityContextSeed.SeedUserAsync(usermanager);
                }
                catch (Exception ex)
                {

                    var logger = loggerFactory.CreateLogger<ApplySeed>();
                    logger.LogError(ex.Message);

                }
            }
        }

    }
}
