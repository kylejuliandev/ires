using System.ComponentModel.DataAnnotations;

namespace Ires.Frontend.Components.Pages.People.Models;

public class CreatePersonModel
{
    [Required]
    [MaxLength(50, ErrorMessage = "The maximum length of a given name is 50 characters.")]
    public string GivenName { get; set; }
}
