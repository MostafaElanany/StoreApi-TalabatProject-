using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Repos.BasketRepository;
using Repos.Interfaces;
using Repos.Repository;
using service.HandlerResponse;
using service.Services.BasketServices.DTO;
using service.Services.BasketServices;
using service.Services.CacheService;
using service.Services.OrderService.Dtos;
using service.Services.ProductServices;
using service.Services.ProductServices.Dtos;
using service.Services.TokenServices;
using service.Services.UserService;
using System.Runtime.CompilerServices;
using service.Services.PaymentService;
using service.Services.OrderService;

namespace StoreApi.Extensions
{
    public static class AppServicesExtenstion
    {

        public static IServiceCollection AddAppServices(this IServiceCollection services )
        {
            services.AddScoped<IUnitWork, UnitWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICacheServices, CacheService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IBasketServices, BasketServices>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            
            services.AddAutoMapper(typeof(BasketProfile));
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(OrderProfile));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                 {
                   var errors = context.ModelState
                                       .Where(e => e.Value.Errors.Count > 0)
                                       .SelectMany(s => s.Value.Errors)
                                       .Select(er => er.ErrorMessage)
                                       .ToList();

                   var errorResponse = new ValidationErrorResponse
                   {
                       Errors = errors
                   };
                   var result = new BadRequestObjectResult(errorResponse);

                   return result;
               };

            });
            return services; 
        }
    }
}
