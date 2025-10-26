using Ires.Api.Endpoints.People.AddAddress;
using Ires.Api.Endpoints.People.CreatePerson;
using Ires.Api.Endpoints.People.GetPeople;

namespace Ires.Api.Endpoints.People;

public static class PeopleEndpoints
{
    public static void MapPeopleEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/people");

        endpoints.MapPost("", CreatePersonEndpoint.ExecuteAsync)
            .MapToApiVersion(1.0)
            .WithName("CreatePerson")
            .WithSummary("Creates a new person")
            .WithDescription("Creates a new person in the system.")
            .WithTags("Person");

        endpoints.MapGet("", GetPeopleEndpoint.ExecuteAsync)
            .MapToApiVersion(1.0)
            .WithName("GetPeople")
            .WithSummary("Retrieves all people with their notes")
            .WithDescription("Fetches people, their details, and any associated notes.")
            .WithTags("Person");

        endpoints.MapPut("/{personId:guid}", AddAddressEndpoint.ExecuteAsync)
            .MapToApiVersion(1.0)
            .WithName("AddAddress")
            .WithSummary("Add Address to person")
            .WithDescription("Associates the address to the person")
            .WithTags("Person");
    }
}
