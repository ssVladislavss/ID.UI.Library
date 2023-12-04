using ID.UI.Components.Base;
using ID.UI.Core.ApiScopes;
using ID.UI.ViewModel.Clients;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.Clients.Edit
{
    public class EditClientComponent : IDBaseComponent
    {
        private List<IDApiScope> _scopes = new List<IDApiScope>();

        [CascadingParameter] protected MudDialogInstance? Instance { get; set; }
        [Parameter] public Client? CurrentClient { get; set; }

        protected IEnumerable<IDApiScope> Scopes
        {
            get
            {
                foreach (var scope in _scopes)
                    yield return scope;
            }
        }
        protected EditClientViewModel Model { get; set; } = new EditClientViewModel();
        protected MudForm ModelForm { get; set; } = new MudForm();


        protected override async Task OnParametersSetAsync()
        {
            if(CurrentClient != null)
            {
                Model = new EditClientViewModel(CurrentClient);
            }

            await base.OnParametersSetAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
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

        protected virtual async Task EditClientAsync()
        {
            OverlayEnabled = true;

            StateHasChanged();

            await ModelForm.Validate();

            if (ModelForm.IsValid)
            {
                var requestModel = Model.ToModel();

                var editResult = await ClientService!.EditAsync(requestModel);

                if (editResult.Result == Core.AjaxResultTypes.Success)
                {
                    Snackbar?.Add("Данные приложения успешно сохранены", Severity.Success);
                    Instance?.Close(requestModel);
                }
                else
                {
                    Snackbar?.Add(editResult.Message, Severity.Error);
                }
            }

            OverlayEnabled = false;

            StateHasChanged();
        }
    }
}
