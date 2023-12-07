using ID.UI.Components.Base;
using ID.UI.Core.Roles;
using ID.UI.Core.Roles.Models;
using ID.UI.ViewModel.Roles;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.Roles.Edit
{
    public class EditRoleComponent : IDBaseComponent
    {
        [CascadingParameter] protected MudDialogInstance? Instance { get; set; }
        [Parameter] public RoleModel? CurrentRole { get; set; }

        protected EditRoleViewModel Model { get; set; } = new EditRoleViewModel();
        protected MudForm ModelForm { get; set; } = new MudForm();

        protected override async Task OnParametersSetAsync()
        {
            if(RoleService is null)
                throw new ArgumentNullException(nameof(RoleService));
            if(CurrentRole is null)
                throw new ArgumentNullException(nameof(CurrentRole));
            else
            {
                Model.RoleId = CurrentRole.RoleId;
                Model.RoleName = CurrentRole.RoleName;
                Model.Claims = CurrentRole.Claims.Select(x => new ViewModel.Claims.ClaimViewModel { Type = x.Type, Value = x.Value }).ToList();
            }

            await base.OnParametersSetAsync();
        }

        protected virtual async Task UpdateRoleAsync()
        {
            await ModelForm.Validate();

            if (ModelForm.IsValid)
            {
                OverlayEnabled = true;

                StateHasChanged();

                var requestModel = new Core.Roles.Models.EditRoleModel
                {
                    RoleId = Model.RoleId,
                    RoleName = Model.RoleName,
                    Claims = Model.Claims.Select(x => new Core.Claims.Models.ClaimModel { Value = x.Value, Type = x.Type })
                };

                var updatedResult = await RoleService!.EditAsync(requestModel);

                if (updatedResult.Result == Core.AjaxResultTypes.Success)
                {
                    Snackbar?.Add("Роль успешно сохранена", Severity.Success);
                    Instance?.Close(requestModel);
                }
                else
                {
                    Snackbar?.Add(updatedResult.Message, Severity.Error);
                }

                OverlayEnabled = false;

                StateHasChanged();
            }
        }
    }
}
