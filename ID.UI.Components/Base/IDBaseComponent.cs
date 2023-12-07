using ID.UI.Core.ApiResources.Abstractions;
using ID.UI.Core.ApiScopes.Abstractions;
using ID.UI.Core.Clients.Abstractions;
using ID.UI.Core.Options.Abstractions;
using ID.UI.Core.Roles.Abstractions;
using ID.UI.Core.State.Abstractions;
using ID.UI.Core.Users.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Net;
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
        [Inject] protected IRoleService? RoleService { get; set; }
        [Inject] protected NavigationManager? Location { get; set; }
        [Inject] protected ISnackbar? Snackbar { get; set; }
        [Inject] protected IDialogService? DialogService { get; set; }

        protected bool OverlayEnabled { get; set; }

        protected ClaimsPrincipal CurrentUser { get; set; } = new ClaimsPrincipal();

        protected override async Task OnParametersSetAsync()
        {
            if (AuthenticationState != null)
            {
                var state = await AuthenticationState;
                if (state != null && state.User != null)
                {
                    CurrentUser = state.User;
                }
            }

            if (ApiResourceService is ITokenHandler resourceTokenHandler)
            {
                resourceTokenHandler!.OnGetToken += OnGetTokenAsync;
                resourceTokenHandler!.OnTokenError += OnTokenErrorAsync;
            }

            if (ApiScopeService is ITokenHandler scopeTokenHandler)
            {
                scopeTokenHandler!.OnGetToken += OnGetTokenAsync;
                scopeTokenHandler!.OnTokenError += OnTokenErrorAsync;
            }

            if (ClientService is ITokenHandler clientTokenHandler)
            {
                clientTokenHandler!.OnGetToken += OnGetTokenAsync;
                clientTokenHandler!.OnTokenError += OnTokenErrorAsync;
            }

            if (UserService is ITokenHandler userTokenHandler)
            {
                userTokenHandler!.OnGetToken += OnGetTokenAsync;
                userTokenHandler!.OnTokenError += OnTokenErrorAsync;
            }

            if (RoleService is ITokenHandler roleTokenHandler)
            {
                roleTokenHandler!.OnGetToken += OnGetTokenAsync;
                roleTokenHandler!.OnTokenError += OnTokenErrorAsync;
            }

            await base.OnParametersSetAsync();
        }
        protected virtual async Task OnTokenErrorAsync(HttpStatusCode? statusCode = null)
        {
            if (statusCode == null)
            {
                await StateService!.LogoutAsync();

                if ($"/{Location!.Uri.Split('/')?.LastOrDefault()}" == "/forbidden" || $"/{Location!.Uri.Split('/')?.LastOrDefault()}" == "/unauthorized")
                    Location?.NavigateTo(Location!.BaseUri, true);
                else
                    Location?.NavigateTo(Location!.Uri, true);
            }
            else if (statusCode == HttpStatusCode.Unauthorized)
            {
                await StateService!.LogoutAsync();

                Location?.NavigateTo("/unauthorized");
            }
            else if (statusCode == HttpStatusCode.Forbidden)
            {
                Location?.NavigateTo("/forbidden");
            }
        }
        protected virtual async Task<string?> OnGetTokenAsync()
        {
            return await StateService!.CurrentTokenAsync();
        }
        protected virtual void ChangeOverlayStatus(bool? status = null)
        {
            if (status.HasValue)
                OverlayEnabled = status.Value;
            else
                OverlayEnabled = !OverlayEnabled;

            StateHasChanged();
        }
    }
}
