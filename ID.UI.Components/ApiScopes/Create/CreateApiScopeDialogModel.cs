using ID.UI.Components.Base;
using ID.UI.Core.ApiScopes.Abstractions;
using ID.UI.ViewModel.ApiScopes;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.ApiScopes.Create
{
    public class CreateApiScopeDialogModel : IDBaseComponent
    {
        protected string? claim;
        [CascadingParameter] protected MudDialogInstance? Instance { get; set; }
        [Inject] protected IApiScopeService? ApiScopeService { get; set; }
        [Inject] protected ISnackbar? Snackbar { get; set; }

        protected CreateApiScopeViewModel Model { get; set; } = new CreateApiScopeViewModel();
        protected MudForm ModelForm { get; set; } = new MudForm();

        protected void AddUserClaim()
        {
            if (string.IsNullOrEmpty(claim))
                return;

            if (Model.UserClaims.Any(x => x == claim))
                return;

            Model.UserClaims.Add(claim);

            claim = string.Empty;

            StateHasChanged();
        }

        protected async Task CreateApiScopeAsync()
        {
            OverlayEnabled = true;

            StateHasChanged();

            await ModelForm.Validate();

            if(ModelForm.IsValid)
            {
                Snackbar!.Add("Форма валидна", Severity.Success);
            }
            else
            {
                Snackbar!.Add("Форма не валидна", Severity.Error);
            }
        }
    }
}
