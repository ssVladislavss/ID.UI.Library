using ID.UI.Components.Base;
using ID.UI.Core.ApiResources;
using ID.UI.Core.ApiResources.Abstractions;
using ID.UI.Core.ApiResources.Models;
using ID.UI.ViewModel.ApiResources;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.ApiResources.Create
{
    public class CreateApiResourceComponent : IDBaseComponent
    {
        protected string? claim;
        protected string? resourceScope;

        [CascadingParameter] MudDialogInstance? Instance { get; set; }
        [Parameter] public bool Show { get; set; }
        [Parameter] public Action<IDApiResource>? CreateComplete { get; set; }

        [Inject] protected IApiResourceService? ApiResourceService { get; set; }

        protected CreateApiResourceViewModel Model { get; set; } = new CreateApiResourceViewModel();
        protected MudForm ModelForm { get; set; } = new MudForm();

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

        protected void AddScope()
        {
            if (string.IsNullOrEmpty(resourceScope))
                return;

            if (Model.Scopes.Any(x => x == resourceScope))
            {
                Snackbar!.Add("Область уже добавлена", Severity.Info);

                return;
            }

            Model.Scopes.Add(resourceScope);

            claim = string.Empty;

            StateHasChanged();
        }

        protected void RemoveLastScope()
        {
            if (Model.Scopes.Any())
            {
                var currentScope = Model.Scopes.Last();
                if (!string.IsNullOrEmpty(currentScope))
                    Model.Scopes.Remove(currentScope);
            }
        }

        protected virtual async Task CreateResourceAsync()
        {
            await ModelForm.Validate();

            if (ModelForm.IsValid)
            {
                OverlayEnabled = true;

                StateHasChanged();

                var requestModel = new CreateApiResourceModel
                {
                    ApiSecrets = Model.ApiSecrets.Select(x => new IdentityServer4.Models.Secret(x.Value, x.Description, x.Expiration)).ToList(),
                    Description = Model.Description,
                    DisplayName = Model.DisplayName,
                    Name = Model.Name,
                    Scopes = Model.Scopes,
                    ShowInDiscoveryDocument = Model.ShowInDiscoveryDocument,
                    UserClaims = Model.UserClaims
                };

                var createResult = await ApiResourceService!.CreateAsync(requestModel);
                if(createResult.Result == Core.AjaxResultTypes.Success && createResult.Data != null)
                {
                    CreateComplete?.Invoke(createResult.Data);
                    Instance?.Close(createResult.Data);
                    Snackbar?.Add("Ресурс успешно создан", Severity.Success);
                }
                else
                {
                    Snackbar?.Add(createResult.Message, Severity.Error);
                }

                OverlayEnabled = false;

                StateHasChanged();
            }
        }
    }
}
