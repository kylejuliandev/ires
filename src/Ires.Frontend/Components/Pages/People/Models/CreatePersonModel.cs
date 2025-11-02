using System.ComponentModel.DataAnnotations;

namespace Ires.Frontend.Components.Pages.People.Models;

public class CreatePersonModel
{
    [Required]
    [MaxLength(50, ErrorMessage = "The maximum length of a given name is 50 characters.")]
    public string GivenName { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "The maximum length of a family name is 100 characters.")]
    public string FamilyName { get; set; }

    [MaxLength(50, ErrorMessage = "The maximum length of a nickname is 50 characters.")]
    public string Nickname { get; set; }

    public CreatePersonModelGender? Gender { get; set; }

    [Required]
    public DateTime? DateOfBirth { get; set; }
}

public enum CreatePersonModelGender
{
    Male,
    Female,
    NonBinary,
    Other
}
