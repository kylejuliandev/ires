namespace Ires.Data;

public class Address
{
    public Guid Id { get; set; }

    public required AddressType Type { get; set; } = AddressType.Unspecified;

    public required string Street { get; set; }

    public required string City { get; set; }

    public required string State { get; set; }

    public required string PostalCode { get; set; }

    public required string Country { get; set; }

    public ICollection<Person> People { get; set; } = [];

    public ICollection<PersonAddresses> PeopleAddresses { get; set; } = [];
}