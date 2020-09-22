using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace NetCore.SwaggerPrototype.App.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.SwaggerDoc(
                    name: "v1.0",
                    info: new OpenApiInfo
                    {
                        Title = ".NET Core - Swagger Prototype",
                        Version = "v1.0"
                    });

                swaggerGenOptions.SwaggerDoc(
                    name: "v2.0",
                    info: new OpenApiInfo
                    {
                        Title = ".NET Core - Swagger Prototype",
                        Version = "v2.0"
                    });

                swaggerGenOptions.AddSecurityDefinition(
                    name: "Bearer",
                    securityScheme: new OpenApiSecurityScheme
                    {
                        Description = "Format: < Bearer ey.. >",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                swaggerGenOptions.AddSecurityRequirement(
                    securityRequirement: new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
                    });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger(swaggerOptions =>
            {
                swaggerOptions.RouteTemplate = "/swagger/{documentname}/swagger.json";
            });

            app.UseSwaggerUI(swaggerOptions =>
            {
                swaggerOptions.SwaggerEndpoint("/swagger/v1.0/swagger.json", $"v1.0");
                swaggerOptions.SwaggerEndpoint("/swagger/v2.0/swagger.json", $"v2.0");

                swaggerOptions.DocumentTitle = ".NET Core - Swagger UI";
                swaggerOptions.RoutePrefix = "swagger";
                swaggerOptions.DisplayRequestDuration();
            });

            return app;
        }
    }
}
