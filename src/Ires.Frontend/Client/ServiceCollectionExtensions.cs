using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace Ires.Frontend.Client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIresClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("iresapi", client =>
        {
            client.BaseAddress = new Uri(configuration["services:iresapi:https:0"]);
        });

        services.AddScoped<IRequestAdapter, HttpClientRequestAdapter>(provider =>
        {
            var authProvider = new AnonymousAuthenticationProvider();
            var httpClient = provider.GetRequiredService<IHttpClientFactory>()
                .CreateClient("iresapi");

            return new HttpClientRequestAdapter(authProvider, httpClient: httpClient);
        });
        services.AddScoped<IresClient>();

        return services;
    }
}
