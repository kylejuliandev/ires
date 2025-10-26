using Ires.Api.Endpoints.Address.CreateAddress;

namespace Ires.Api.Endpoints.Address;

public static class AddressEndpoints
{
    public static void MapAddressEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/address");

        endpoints.MapPost("", CreateAddressEndpoint.ExecuteAsync)
            .MapToApiVersion(1.0)
            .WithName("CreateAddress")
            .WithSummary("Creates a new address")
            .WithDescription("Creates a new address in the system.")
            .WithTags("Address");
    }
}
