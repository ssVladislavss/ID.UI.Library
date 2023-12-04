using ID.UI.Core.ApiResources.Abstractions;
using ID.UI.Core.ApiScopes.Abstractions;
using ID.UI.Core.Clients.Abstractions;
using ID.UI.Core.State.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Security.Claims;

namespace ID.UI.Components.Base
{
    public class IDBaseComponent : ComponentBase
    {
        [CascadingParameter] protected Task<AuthenticationState>? AuthenticationState { get; set; }
        [Inject] protected IStateService? StateService { get; set; }
        [Inject] protected IApiResourceService? ApiResourceService { get; set; }
        [Inject] protected IApiScopeService? ApiScopeService { get; set; }
        [Inject] protected IClientService? ClientService { get; set; }
        [Inject] protected NavigationManager? Location { get; set; }
        [Inject] protected ISnackbar? Snackbar { get; set; }
        [Inject] protected IDialogService? DialogService { get; set; }

        protected bool OverlayEnabled { get; set; }

        protected ClaimsPrincipal CurrentUser { get; set; } = new ClaimsPrincipal();

        protected override async Task OnParametersSetAsync()
        {
            if(AuthenticationState != null)
            {
                var state = await AuthenticationState;
                if(state != null && state.User != null)
                {
                    CurrentUser = state.User;
                }
            }

            ApiResourceService!.OnGetToken += OnGetToken;
            ApiResourceService!.OnTokenError += OnTokenError;

            ApiScopeService!.OnGetToken += OnGetToken;
            ApiScopeService!.OnTokenError += OnTokenError;

            ClientService!.OnGetToken += OnGetToken;
            ClientService!.OnTokenError += OnTokenError;

            await base.OnParametersSetAsync();
        }
        protected virtual async Task OnTokenError()
        {
            await StateService!.LogoutAsync();
            Location?.NavigateTo(Location!.BaseUri, true);
        }

        protected virtual async Task<string?> OnGetToken()
        {
            return await StateService!.CurrentTokenAsync();
        }
    }
}
