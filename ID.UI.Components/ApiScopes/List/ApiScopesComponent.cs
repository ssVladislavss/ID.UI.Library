using ID.UI.Components.ApiScopes.Create;
using ID.UI.Components.ApiScopes.Edit;
using ID.UI.Components.Base;
using ID.UI.Core.ApiScopes;
using ID.UI.Core.ApiScopes.Models;
using MudBlazor;

namespace ID.UI.Components.ApiScopes.List
{
    public class ApiScopesComponent : IDBaseComponent
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
        protected async Task EditScopeAsync(IDApiScope apiScope)
        {
            var dialogInstanse = await DialogService!.ShowAsync<EditApiScopeDialog>("", new DialogParameters()
            {
                ["CurrentScope"] = apiScope
            }, new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = false,
                DisableBackdropClick = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            });

            var dialogResult = await dialogInstanse.Result;
            if (dialogResult != null)
                if (dialogResult.Data is EditApiScopeModel changedApiScope)
                {
                    apiScope.Set(changedApiScope);
                }
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

                Snackbar!.Add("Статус изменён", Severity.Success);
            }

            OverlayEnabled = false;

            StateHasChanged();
        }
        protected async Task CreateApiScopeDialogShowAsync()
        {
            var dialogInstanse = await DialogService!.ShowAsync<CreateApiScopeDialog>("", new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = false,
                DisableBackdropClick = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            });

            var dialogResult = await dialogInstanse.Result;
            if(dialogResult != null)
                if(dialogResult.Data is IDApiScope addedApiScope)
                {
                    _scopes.Add(addedApiScope);
                }
        }
        protected async Task RemoveScopesAsync()
        {
            if(SelectedScopes.Count > 0)
            {
                OverlayEnabled = true;

                StateHasChanged();

                foreach(var scope in SelectedScopes)
                {
                    var removeResult = await ApiScopeService!.RemoveAsync(scope.Id);
                    if(removeResult.Result == Core.AjaxResultTypes.Error)
                    {
                        Snackbar!.Add($"Не удалось удалить область - {scope.Name}", Severity.Error);
                    }
                    else
                    {
                        _scopes.Remove(scope);
                        Snackbar!.Add($"Область - {scope.Name} - удалена", Severity.Success);
                    }
                }

                OverlayEnabled = false;

                StateHasChanged();
            }
        }
    }
}
