using ID.UI.Components.Base;
using ID.UI.Core.ApiScopes;
using ID.UI.ViewModel.Clients;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.Clients.Create
{
    public class CreateClientComponent : IDBaseComponent
    {
        private List<IDApiScope> _scopes = new List<IDApiScope>();

        protected IEnumerable<IDApiScope> Scopes
        {
            get
            {
                foreach (var scope in _scopes)
                    yield return scope;
            }
        }
        protected MudForm ModelForm { get; set; } = new MudForm();
        protected CreateClientViewModel Model { get; set; } = new CreateClientViewModel();

        [CascadingParameter] protected MudDialogInstance? Instance { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                await GetApiScopesAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected virtual async Task GetApiScopesAsync()
        {
            OverlayEnabled = true;

            StateHasChanged();

            var sendResult = await ApiScopeService!.GetAsync(new ApiScopeSearchFilter());

            if (sendResult.Result == Core.AjaxResultTypes.Success && sendResult.Data != null)
            {
                _scopes.AddRange(sendResult.Data);
            }

            OverlayEnabled = false;

            StateHasChanged();
        }

        protected virtual async Task CreateClientAsync()
        {
            OverlayEnabled = true;

            StateHasChanged();

            await ModelForm.Validate();

            if (ModelForm.IsValid)
            {
                var requestModel = Model.ToModel();

                var createResult = await ClientService!.CreateAsync(requestModel);

                if(createResult.Result == Core.AjaxResultTypes.Success && createResult.Data != null)
                {
                    Snackbar?.Add("Приложение успешно создано", Severity.Success);
                    Instance?.Close(createResult.Data);
                }
                else
                {
                    Snackbar?.Add(createResult.Message, Severity.Error);
                }
            }

            OverlayEnabled = false;

            StateHasChanged();
        }
    }
}
