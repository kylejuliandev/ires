namespace Ires.Data;

public class PersonNote
{
    public Guid Id { get; set; }

    public string Content { get; set; }

    public Guid PersonId { get; set; }

    public Person Person { get; set; }
}