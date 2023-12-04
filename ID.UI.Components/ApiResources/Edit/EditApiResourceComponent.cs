using ID.UI.Components.Base;
using ID.UI.Core.ApiResources;
using ID.UI.Core.ApiResources.Models;
using ID.UI.Core.ApiScopes;
using ID.UI.ViewModel.ApiResources;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.ApiResources.Edit
{
    public class EditApiResourceComponent : IDBaseComponent
    {
        protected string? claim;

        [CascadingParameter] protected MudDialogInstance? Instance { get; set; }
        [Parameter] public IDApiResource? CurrentResource { get; set; }
        [Parameter] public IEnumerable<IDApiScope> Scopes { get; set; } = new List<IDApiScope>();

        protected EditApiResourceViewModel Model { get; set; } = new EditApiResourceViewModel();
        protected MudForm ModelForm { get; set; } = new MudForm();

        protected override async Task OnParametersSetAsync()
        {
            if(CurrentResource != null)
            {
                Model = new EditApiResourceViewModel(CurrentResource);
            }

            await base.OnParametersSetAsync();
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

        protected void SelectedApiScopesChanged(IEnumerable<string> apiScopes)
        {
            Model.Scopes = apiScopes.ToList();
        }

        protected virtual async Task EditResourceAsync()
        {
            await ModelForm.Validate();

            if (ModelForm.IsValid)
            {
                OverlayEnabled = true;

                StateHasChanged();

                var requestModel = new EditApiResourceModel
                {
                    ApiSecrets = Model.ApiSecrets.Select(x => new IdentityServer4.Models.Secret(x.Value, x.Description, x.Expiration)).ToList(),
                    Description = Model.Description,
                    DisplayName = Model.DisplayName,
                    Id = Model.Id,
                    Name = Model.Name,
                    Scopes = Model.Scopes,
                    ShowInDiscoveryDocument = Model.ShowInDiscoveryDocument,
                    UserClaims = Model.UserClaims
                };

                var editResult = await ApiResourceService!.EditAsync(requestModel);
                if(editResult.Result == Core.AjaxResultTypes.Success)
                {
                    Snackbar?.Add("Ресурс успешно сохранен", Severity.Success);
                    Instance?.Close(requestModel);
                }
                else
                {
                    Snackbar?.Add(editResult.Message, Severity.Error);
                }

                OverlayEnabled = false;

                StateHasChanged();
            }
        }
    }
}
