namespace Ires.Data;

public class User
{
    public Guid Id { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }

    public ICollection<Person> People { get; set; } = [];

    public ICollection<Address> Addresses { get; set; } = [];
}
