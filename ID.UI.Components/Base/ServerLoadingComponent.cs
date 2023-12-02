using Microsoft.AspNetCore.Components;

namespace ID.UI.Components.Base
{
    public class ServerLoadingComponent : ComponentBase
    {
        [Parameter] public bool Show { get; set; }
    }
}
