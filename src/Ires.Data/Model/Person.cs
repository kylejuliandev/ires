namespace Ires.Data;

public class Person
{
    public Guid Id { get; set; }

    public required string GivenName { get; set; }

    public required string FamilyName { get; set; }

    public string? Nickname { get; set; }

    public required DateOnly DateOfBirth { get; set; }

    public ICollection<PersonNote> Notes { get; set; } = [];

    public ICollection<Address> Addresses { get; set; } = [];
    
    public ICollection<PersonAddresses> PersonAddresses { get; set; } = [];

    public ICollection<ContactDetail> Contacts { get; set; } = [];

    public Guid CreatedById { get; set; }

    public User CreatedBy { get; set; }
}
