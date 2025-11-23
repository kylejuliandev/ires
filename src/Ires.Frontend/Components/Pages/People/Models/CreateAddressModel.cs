using System.ComponentModel.DataAnnotations;

namespace Ires.Frontend.Components.Pages.People.Models;

public class CreateAddressModel
{
    [Required(ErrorMessage = "You must specify a Street.")]
    [MaxLength(250, ErrorMessage = "The maximum length of a Street is 250 characters.")]
    public string Street { get; set; }

    [Required(ErrorMessage = "You must specify a Street.")]
    [MaxLength(100, ErrorMessage = "The maximum length of a Street is 100 characters.")]
    public string City { get; set; }

    [Required(ErrorMessage = "You must specify a State.")]
    [MaxLength(100, ErrorMessage = "The maximum length of a State is 100 characters.")]
    public string State { get; set; }

    [Required(ErrorMessage = "You must specify a Postal code.")]
    [MaxLength(20, ErrorMessage = "The maximum length of a Postal code is 20 characters.")]
    public string PostalCode { get; set; }

    [Required(ErrorMessage = "You must specify a Country.")]
    [MaxLength(100, ErrorMessage = "The maximum length of a Country is 100 characters.")]
    public string Country { get; set; }

    [Required(ErrorMessage = "You must specify a Address Type.")]
    public AddressTypeModel AddressType { get; set; } = AddressTypeModel.Home;
}

public enum AddressTypeModel
{
    Other,
    Home,
    Work
}
