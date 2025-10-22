namespace Ires.Data;

public class ContactDetail
{
    public Guid Id { get; set; }

    public required Guid PersonId { get; set; }

    public Person Person { get; set; }

    public ContactType Type { get; set; } = ContactType.Unspecified;

    public required string Value { get; set; }
}