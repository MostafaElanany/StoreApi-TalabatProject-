using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace StoreApi.Extensions
{
    public static class SawggerEX
    {
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(ops =>
            {
                ops.SwaggerDoc("v1", new OpenApiInfo { Title = "StoreApi", Version = "v1" });

                var  securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearer"
                    }
                };

                ops.AddSecurityDefinition("bearer", securityScheme);

                var securityReq = new OpenApiSecurityRequirement 
                {
                    { securityScheme , new [] { "bearer" } }

                };
                ops.AddSecurityRequirement(securityReq);



        });
            return services;
        }

    }
}
