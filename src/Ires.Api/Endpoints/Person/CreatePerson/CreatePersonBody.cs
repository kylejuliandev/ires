using System.Text.Json.Serialization;

namespace Ires.Api.Endpoints.Person.CreatePerson;

public record CreatePersonBody
{
    [JsonPropertyName("given_name")]
    public required string GivenName { get; init; }

    [JsonPropertyName("family_name")]
    public required string FamilyName { get; init; }

    [JsonPropertyName("nickname")]
    public string? Nickname { get; init; }

    [JsonPropertyName("gender")]
    public string Gender { get; init; } = "NotSpecified";

    [JsonPropertyName("date_of_birth")]
    public required DateOnly DateOfBirth { get; init; }

    [JsonPropertyName("notes")]
    public string Notes { get; init; } = string.Empty;
}
