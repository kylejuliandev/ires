namespace Ires.Data;

public class Person
{
    public Guid Id { get; set; }

    public required string GivenName { get; set; }

    public required string FamilyName { get; set; }

    public string? Nickname { get; set; }

    public required Gender Gender { get; set; }

    public required DateOnly DateOfBirth { get; set; }

    public ICollection<PersonNote> Notes { get; set; } = new List<PersonNote>();
}
