using System.ComponentModel.DataAnnotations;

namespace Ires.Frontend.Components.Pages.Models;

public class SignUpModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
