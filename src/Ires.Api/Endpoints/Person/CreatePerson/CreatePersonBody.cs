using System.Text.Json.Serialization;

namespace Ires.Api.Endpoints.Person.CreatePerson;

public record CreatePersonBody
{
    [JsonPropertyName("first_name")]
    public required string FirstName { get; init; }

    [JsonPropertyName("last_name")]
    public required string LastName { get; init; }

    [JsonPropertyName("middle_name")]
    public string? MiddleName { get; init; }

    [JsonPropertyName("nickname")]
    public string? Nickname { get; init; }

    [JsonPropertyName("gender")]
    public string Gender { get; init; } = "NotSpecified";

    [JsonPropertyName("date_of_birth")]
    public required DateOnly DateOfBirth { get; init; }

    [JsonPropertyName("notes")]
    public string Notes { get; init; } = string.Empty;
}
