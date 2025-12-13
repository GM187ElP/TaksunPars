using Microsoft.AspNetCore.Components;

namespace PersonellInfo.Blazor.Components.Services;


public class WizardStepBase : ComponentBase
{
    [Parameter]
    public EventCallback OnNext { get; set; }

    [Parameter]
    public EventCallback OnPrevious { get; set; }
}
