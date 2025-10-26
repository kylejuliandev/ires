using Ires.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ires.Api.Endpoints.People.AddAddress;

public static class AddAddressEndpoint
{
    public static async Task<Results<NoContent, BadRequest<ProblemDetails>, NotFound<ProblemDetails>>> ExecuteAsync(
        [FromRoute] Guid personId, 
        [FromBody] AddAddressBody request,
        [FromServices] IresDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var person = await dbContext.People.FindAsync([personId], cancellationToken);

        if (person is null)
        {
            return TypedResults.NotFound(new ProblemDetails()
            {
                Title = "Person not found",
                Detail = $"No person found with ID {personId}"
            });
        }

        if (person.Addresses.Any(a => a.Id == request.AddressId))
        {
            return TypedResults.NoContent();
        }

        var address = await dbContext.Addresses.FindAsync([request.AddressId], cancellationToken);
        if (address is null)
        {
            return TypedResults.BadRequest(new ProblemDetails()
            {
                Title = "Address not found",
                Detail = $"No address found with ID {request.AddressId}"
            });
        }

        person.Addresses.Add(address);
        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }
}
