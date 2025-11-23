namespace Ires.Data;

public class PersonAddresses
{
    public Guid PersonId { get; set; }

    public Guid AddressId { get; set; }

    public DateTimeOffset AddedOn { get; set; } = DateTimeOffset.UtcNow;
}
