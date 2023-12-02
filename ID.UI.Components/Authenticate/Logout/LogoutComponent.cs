using ID.UI.Components.Base;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.Authenticate.Logout
{
    public class LogoutComponent : IDBaseComponent
    {
        [Parameter] public Size Size { get; set; } = Size.Medium;
        [Parameter] public Color Color { get; set; } = Color.Primary;
        [Parameter] public bool DisableElevation { get; set; }

        protected async Task LogoutAsync()
        {
            OverlayEnabled = true;

            StateHasChanged();

            await StateService!.LogoutAsync();

            Location?.NavigateTo(Location!.Uri, true);
        }
    }
}
