# .NET - Swagger

## Install
``` powershell
PM> Install-Package Microsoft.AspNetCore.Mvc.Versioning -Version 4.2.0
PM> Install-Package Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer -Version 4.2.0
PM> Install-Package Swashbuckle.AspNetCore.Swagger -Version 5.6.3
PM> Install-Package Swashbuckle.AspNetCore.SwaggerGen -Version 5.6.3
PM> Install-Package Swashbuckle.AspNetCore.SwaggerUi -Version 5.6.3
```

## Extensions
``` csharp
public static class ApiVersioningExtensions
{
    public static IServiceCollection AddApiVersions(this IServiceCollection services)
    {
        services.AddApiVersioning(apiVersioningOptions =>
        {
            apiVersioningOptions.AssumeDefaultVersionWhenUnspecified = true;
            apiVersioningOptions.DefaultApiVersion = new ApiVersion(1, 0);
            apiVersioningOptions.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(apiExplorerOptions =>
        {
            apiExplorerOptions.GroupNameFormat = "'v'VVVV";
            apiExplorerOptions.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}
```

``` csharp
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
```

## Usage
``` csharp
public class Startup
{
    // ..

    public void ConfigureServices(IServiceCollection services)
    {
        // ..
        
        services.AddControllers();
        services.AddApiVersions();
        services.AddSwagger();
        
        // ..
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // ..
        
        app.UseSwaggerDoc();
        
        // ..
    }
}
```

## Swagger UI sample
![Foo1](https://github.com/dimitrietataru/netcore-swagger/blob/master/images/Swagger%20-%20Foo%20v1.png)
![Foo2](https://github.com/dimitrietataru/netcore-swagger/blob/master/images/Swagger%20-%20Foo%20v2.png)
