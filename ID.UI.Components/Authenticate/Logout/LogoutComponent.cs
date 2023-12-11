using ID.UI.Components.Base;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net;

namespace ID.UI.Components.Authenticate.Logout
{
    public class LogoutComponent : IDBaseComponent
    {
        [Parameter] public Size Size { get; set; } = Size.Medium;
        [Parameter] public Color Color { get; set; } = Color.Primary;
        [Parameter] public bool DisableElevation { get; set; }

        protected override async Task OnTokenErrorAsync(HttpStatusCode? statusCode = null)
        {
            ChangeOverlayStatus();

            await base.OnTokenErrorAsync(statusCode);
        }
    }
}
