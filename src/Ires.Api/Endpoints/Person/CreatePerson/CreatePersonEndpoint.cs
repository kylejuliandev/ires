using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ires.Api.Endpoints.Person.CreatePerson;

public static class CreatePersonEndpoint
{
    public static async Task<Results<NoContent, BadRequest<ProblemDetails>>> MapCreatePersonEndpoint(
        [FromBody] CreatePersonBody request)
    {
        return TypedResults.NoContent();
    }
}
