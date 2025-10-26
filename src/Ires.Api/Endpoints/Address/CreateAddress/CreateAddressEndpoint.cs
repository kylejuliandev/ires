using Ires.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ires.Api.Endpoints.Address.CreateAddress;

public static class CreateAddressEndpoint
{
    public static async Task<Results<NoContent, BadRequest<ProblemDetails>>> ExecuteAsync(
        [FromBody] CreateAddressBody request,
        [FromServices] IresDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var address = new Data.Address()
        {
            Id = Guid.NewGuid(),
            Type = ToAddressType(request.AddressType),
            Street = request.Street,
            City = request.City,
            State = request.State,
            PostalCode = request.PostalCode,
            Country = request.Country
        };
        dbContext.Addresses.Add(address);

        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }

    private static AddressType ToAddressType(string addressType) => addressType switch
    {
        "Home" => AddressType.Home,
        "Work" => AddressType.Work,
        _ => AddressType.Unspecified
    };
}
