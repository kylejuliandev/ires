using Ires.Data;

namespace Ires.Frontend.Components.Pages.People.Models;

public static class CreatePersonModelExtensions
{
    extension(CreatePersonModelGender? gender)
    {
        public Gender GetGender() => gender switch
        {
            CreatePersonModelGender.Male => Ires.Data.Gender.Male,
            CreatePersonModelGender.Female => Gender.Female,
            CreatePersonModelGender.NonBinary => Gender.NonBinary,
            CreatePersonModelGender.Other => Gender.NotSpecified,
            _ => Gender.NotSpecified
        };
    }

    extension(Gender gender)
    {
        public CreatePersonModelGender? GetModelGender() => gender switch
        {
            Gender.Male => CreatePersonModelGender.Male,
            Gender.Female => CreatePersonModelGender.Female,
            Gender.NonBinary => CreatePersonModelGender.NonBinary,
            Gender.Other => CreatePersonModelGender.Other,
            _ => CreatePersonModelGender.Other
        };
    }
}