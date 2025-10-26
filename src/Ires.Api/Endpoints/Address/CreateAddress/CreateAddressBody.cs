using System.Text.Json.Serialization;

namespace Ires.Api.Endpoints.Address.CreateAddress;

public record CreateAddressBody
{
    [JsonPropertyName("address_type")]
    public required string AddressType { get; init; }

    [JsonPropertyName("street")]
    public required string Street { get; init; }

    [JsonPropertyName("city")]
    public required string City { get; init; }

    [JsonPropertyName("state")]
    public required string State { get; init; }

    [JsonPropertyName("postal_code")]
    public required string PostalCode { get; init; }

    [JsonPropertyName("country")]
    public required string Country { get; init; }
}
