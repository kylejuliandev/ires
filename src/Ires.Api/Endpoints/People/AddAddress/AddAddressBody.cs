using System.Text.Json.Serialization;

namespace Ires.Api.Endpoints.People.AddAddress;

public record AddAddressBody
{
    [JsonPropertyName("address_id")]
    public required Guid AddressId { get; init; }
}
