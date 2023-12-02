using ID.UI.Components.Base;
using ID.UI.Core.ApiScopes;
using ID.UI.Core.ApiScopes.Abstractions;
using ID.UI.Core.ApiScopes.Models;
using ID.UI.ViewModel.ApiScopes;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.ApiScopes.Edit
{
    public class EditApiScopeDialogComponent : IDBaseComponent
    {
        protected string? claim;

        [CascadingParameter] protected MudDialogInstance? Instance { get; set; }
        [Parameter] public IDApiScope? CurrentScope { get; set; }
        [Inject] protected IApiScopeService? ApiScopeService { get; set; }

        protected EditApiScopeViewModel Model { get; set; } = new EditApiScopeViewModel();
        protected MudForm ModelForm { get; set; } = new MudForm();

        protected override async Task OnInitializedAsync()
        {
            Model = new EditApiScopeViewModel(CurrentScope!);

            await base.OnInitializedAsync();
        }

        protected void AddUserClaim()
        {
            if (string.IsNullOrEmpty(claim))
                return;

            if (Model.UserClaims.Any(x => x == claim))
            {
                Snackbar!.Add("Утверждение уже добавлено", Severity.Info);

                return;
            }

            Model.UserClaims.Add(claim);

            claim = string.Empty;

            StateHasChanged();
        }

        protected void RemoveLastUserClaim()
        {
            if (Model.UserClaims.Any())
            {
                var currentClaim = Model.UserClaims.Last();
                if (!string.IsNullOrEmpty(currentClaim))
                    Model.UserClaims.Remove(currentClaim);
            }
        }

        protected async Task EditScopeAsync()
        {
            await ModelForm.Validate();

            if(ModelForm.IsValid)
            {
                OverlayEnabled = true;

                StateHasChanged();

                var requestModel = new EditApiScopeModel
                {
                    Description = Model.Description,
                    DisplayName = Model.DisplayName,
                    Emphasize = Model.Emphasize,
                    Id = Model.Id,
                    Name = Model.Name,
                    Required = Model.Required,
                    ShowInDiscoveryDocument = Model.ShowInDiscoveryDocument,
                    UserClaims = Model.UserClaims
                };

                var requestResult = await ApiScopeService!.EditAsync(requestModel);
                if(requestResult.Result == Core.AjaxResultTypes.Success)
                {
                    Snackbar!.Add("Данные области успешно сохранены", Severity.Success);
                    Instance!.Close(requestModel);
                }
                else
                {
                    Snackbar!.Add("Не удалось сохранить данные области", Severity.Error);
                }

                OverlayEnabled = false;

                StateHasChanged();
            }
        }
    }
}
