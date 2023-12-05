using ID.UI.Core.ApiResources.Abstractions;
using ID.UI.Core.ApiScopes.Abstractions;
using ID.UI.Core.Clients.Abstractions;
using ID.UI.Core.Options.Abstractions;
using ID.UI.Core.State.Abstractions;
using ID.UI.Core.Users.Abstractions;
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
        [Inject] protected IUserService? UserService { get; set; }
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

            if(ApiResourceService is ITokenHandler resourceTokenHandler)
            {
                resourceTokenHandler!.OnGetToken += OnGetToken;
                resourceTokenHandler!.OnTokenError += OnTokenError;
            }

            if (ApiScopeService is ITokenHandler scopeTokenHandler)
            {
                scopeTokenHandler!.OnGetToken += OnGetToken;
                scopeTokenHandler!.OnTokenError += OnTokenError;
            }

            if (ClientService is ITokenHandler clientTokenHandler)
            {
                clientTokenHandler!.OnGetToken += OnGetToken;
                clientTokenHandler!.OnTokenError += OnTokenError;
            }

            if(UserService is ITokenHandler userTokenHandler)
            {
                userTokenHandler!.OnGetToken += OnGetToken;
                userTokenHandler!.OnTokenError += OnTokenError;
            }

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
