using Ires.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ires.Api.Endpoints.People.CreatePerson;

public static class CreatePersonEndpoint
{
    public static async Task<Results<NoContent, BadRequest<ProblemDetails>>> ExecuteAsync(
        [FromBody] CreatePersonBody request,
        [FromServices] IresDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var person = new Person
        {
            Id = Guid.NewGuid(),
            GivenName = request.GivenName,
            FamilyName = request.FamilyName,
            Nickname = request.Nickname,
            Gender = ToGender(request.Gender),
            DateOfBirth = request.DateOfBirth,
        };

        if (!string.IsNullOrWhiteSpace(request.Notes))
        {
            var note = new PersonNote
            {
                Id = Guid.NewGuid(),
                Content = request.Notes,
                PersonId = person.Id,
                Person = person
            };

            person.Notes.Add(note);

            dbContext.PeopleNotes.Add(note);
        }

        dbContext.People.Add(person);

        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }

    private static Gender ToGender(string gender) => gender switch
    {
        "Male" => Gender.Male,
        "Female" => Gender.Female,
        "NonBinary" => Gender.NonBinary,
        "Other" => Gender.Other,
        _ => Gender.NotSpecified
    };
}
