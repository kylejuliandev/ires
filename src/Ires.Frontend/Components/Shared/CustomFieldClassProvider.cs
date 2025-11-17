using Microsoft.AspNetCore.Components.Forms;

namespace Ires.Frontend.Components.Shared;

public class CustomFieldClassProvider : FieldCssClassProvider
{
    public override string GetFieldCssClass(EditContext editContext,
        in FieldIdentifier fieldIdentifier)
    {
        var isValid = editContext.IsValid(fieldIdentifier);

        return isValid ? string.Empty : "is-invalid";
    }
}