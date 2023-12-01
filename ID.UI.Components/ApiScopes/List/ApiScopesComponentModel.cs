using ID.UI.Components.ApiScopes.Create;
using ID.UI.Components.Base;
using ID.UI.Core.ApiScopes;
using ID.UI.Core.ApiScopes.Abstractions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.ApiScopes.List
{
    public class ApiScopesComponentModel : IDBaseComponent
    {
        private List<IDApiScope> _scopes = new();

        protected MudTable<IDApiScope> ScopesTable = new();

        protected IEnumerable<IDApiScope> Scopes
        {
            get
            {
                foreach (var scope in _scopes)
                    yield return scope;
            }
        }
        protected HashSet<IDApiScope> SelectedScopes { get; set; } = new HashSet<IDApiScope>();

        [Inject] protected IApiScopeService? ApiScopeService { get; set; }
        [Inject] protected NavigationManager? Location { get; set; }
        [Inject] protected IDialogService? DialogService { get; set; }

        protected override Task OnInitializedAsync()
        {
            ApiScopeService!.OnGetToken += OnGetToken;
            ApiScopeService!.OnTokenError += OnTokenError;

            return base.OnInitializedAsync();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetApiScopesAsync();
            }
        }

        protected async Task GetApiScopesAsync()
        {
            OverlayEnabled = true;

            StateHasChanged();

            var sendResult = await ApiScopeService!.GetAsync(new ApiScopeSearchFilter());
            if(sendResult.Result == Core.AjaxResultTypes.Success && sendResult.Data != null)
            {
                _scopes = sendResult.Data.ToList();
            }

            OverlayEnabled = false;

            StateHasChanged();
        }
        protected async Task EditStatusAsync(IDApiScope apiScope)
        {
            OverlayEnabled = true;

            StateHasChanged();

            var sendResult = await ApiScopeService!.EditStatusAsync(new Core.ApiScopes.Models.EditApiScopeStatusModel
            {
                Id = apiScope.Id,
                Status = !apiScope.Enabled
            });

            if(sendResult.Result == Core.AjaxResultTypes.Success)
            {
                apiScope.Enabled = !apiScope.Enabled;
            }

            await Task.Delay(3000);

            OverlayEnabled = false;

            StateHasChanged();
        }
        protected async Task CreateApiScopeDialogShowAsync()
        {
            var dialogResult = await DialogService!.ShowAsync<CreateApiScopeDialog>("", new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = false,
                DisableBackdropClick = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            });
        }
    }
}
