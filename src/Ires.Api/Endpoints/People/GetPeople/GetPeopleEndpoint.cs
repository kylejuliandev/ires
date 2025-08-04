using Ires.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ires.Api.Endpoints.People.GetPeople;

public static class GetPeopleEndpoint
{
    public static async Task<Ok<GetPeopleResponse>> ExecuteAsync(
        [FromServices] IresDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var people = await dbContext.People.Include(p => p.Notes).ToListAsync(cancellationToken);

        var mappedPeople = people.Select(p => new GetPeoplePerson
        {
            Id = p.Id,
            GivenName = p.GivenName,
            FamilyName = p.FamilyName,
            Nickname = p.Nickname,
            Gender = ToGenderString(p.Gender),
            DateOfBirth = p.DateOfBirth,
            Notes = [.. p.Notes.Select(n => n.Content)]
        });

        return TypedResults.Ok(new GetPeopleResponse
        {
            People = [.. mappedPeople]
        });
    }

    private static string ToGenderString(Gender gender) => gender switch
    {
        Gender.Male => "Male",
        Gender.Female => "Female",
        Gender.NonBinary => "NonBinary",
        Gender.Other => "Other",
        _ => "NotSpecified"
    };
}
