using Asp.Versioning;

namespace Ires.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIresApiVersioning(this IServiceCollection services)
    {
        services
            .AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.ReportApiVersions = true;
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }
}
