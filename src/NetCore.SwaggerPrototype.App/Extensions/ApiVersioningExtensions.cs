using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace NetCore.SwaggerPrototype.App.Extensions
{
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
}
