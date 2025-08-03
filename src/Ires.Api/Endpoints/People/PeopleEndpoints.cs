using Ires.Api.Endpoints.People.CreatePerson;

namespace Ires.Api.Endpoints.People;

public static class PeopleEndpoints
{
    public static void MapPeopleEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/people");

        endpoints.MapPost("", CreatePersonEndpoint.MapCreatePersonEndpoint)
            .MapToApiVersion(1.0)
            .WithName("CreatePerson")
            .WithSummary("Creates a new person")
            .WithDescription("Creates a new person in the system.")
            .WithTags("Person");
    }
}
