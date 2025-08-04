using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace Ires.Frontend.Client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIresClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRequestAdapter, HttpClientRequestAdapter>(provider =>
        {
            var authProvider = new AnonymousAuthenticationProvider();
            var adapter = new HttpClientRequestAdapter(authProvider)
            {
                BaseUrl = configuration["services:ires-api:http:0"]
            };

            return adapter;
        });
        services.AddScoped<IresClient>();

        return services;
    }
}
