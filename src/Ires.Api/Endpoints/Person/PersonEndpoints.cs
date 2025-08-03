using Ires.Api.Endpoints.Person.CreatePerson;

namespace Ires.Api.Endpoints.Person;

public static class PersonEndpoints
{
    public static void MapPersonEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/person");

        endpoints.MapPost("", CreatePersonEndpoint.MapCreatePersonEndpoint)
            .MapToApiVersion(1.0)
            .WithName("CreatePerson")
            .WithSummary("Creates a new person")
            .WithDescription("Creates a new person in the system.")
            .WithTags("Person");
    }
}
