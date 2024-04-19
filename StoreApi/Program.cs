using dataa.context;
using dataa.Entities.IdentityEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repos.BasketRepository;
using Repos.Interfaces;
using Repos.Repository;
using service.Services.BasketServices;
using service.Services.BasketServices.DTO;
using service.Services.CacheService;
using service.Services.OrderService.Dtos;
using service.Services.ProductServices;
using service.Services.ProductServices.Dtos;
using service.Services.TokenServices;
using service.Services.UserService;
using StackExchange.Redis;
using StoreApi.Extensions;
using StoreApi.Helper;
using StoreApi.Middlewares;
using Stripe;

namespace StoreApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<storecontext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddAppServices();

            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var configration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(configration);
            }
            );
            builder.Services.AddIdentityServices(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerDoc();
            builder.Services.AddCors(op=>
                {   op.AddPolicy("CorsPolicy", policy =>
                        {
                            policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200/", "https://localhost:7144/");
                        });
                });

            var app = builder.Build();
            await ApplySeed.applyseedingasync(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
