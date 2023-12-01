using ID.UI.Components.Base;
using ID.UI.Core.ApiScopes.Abstractions;
using ID.UI.Core.ApiScopes.Models;
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
        protected MudForm ModelForm { get; set; } = new MudForm() { IsValid = false };

        protected void AddUserClaim()
        {
            if (string.IsNullOrEmpty(claim))
                return;

            if (Model.UserClaims.Any(x => x == claim))
            {
                Snackbar!.Add("Утверждение уже добавлено", Severity.Info, opt => opt.HideTransitionDuration = 1);

                return;
            }

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
                var requestModel = new CreateApiScopeModel()
                {
                    Description = Model.Description,
                    DisplayName = Model.DisplayName,
                    Emphasize = Model.Emphasize,
                    Name = Model.Name,
                    Required = Model.Required,
                    ShowInDiscoveryDocument = Model.ShowInDiscoveryDocument,
                    UserClaims = Model.UserClaims
                };

                var requestResult = await ApiScopeService!.CreateAsync(requestModel);
                if(requestResult.Result == Core.AjaxResultTypes.Success && requestResult.Data != null)
                {
                    Snackbar!.Add("Область успешно создана!", Severity.Success);
                    Instance!.Close(requestResult.Data);
                }
                else
                {
                    Snackbar!.Add(requestResult.Message, Severity.Error);
                }
            }
        }
    }
}
