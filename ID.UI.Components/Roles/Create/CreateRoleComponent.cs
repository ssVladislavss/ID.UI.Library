using ID.UI.Components.Base;
using ID.UI.ViewModel.Roles;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.Roles.Create
{
    public class CreateRoleComponent : IDBaseComponent
    {
        [CascadingParameter] protected MudDialogInstance? Instance { get; set; }

        protected CreateRoleViewModel Model { get; set; } = new CreateRoleViewModel();
        protected MudForm ModelForm { get; set; } = new MudForm();

        protected virtual async Task CreateRoleAsync()
        {
            await ModelForm.Validate();

            if(ModelForm.IsValid)
            {
                OverlayEnabled = true;

                StateHasChanged();

                var createdResult = await RoleService!.CreateAsync(new Core.Roles.Models.CreateRoleModel
                {
                    RoleName = Model.RoleName
                });

                if (createdResult.Result == Core.AjaxResultTypes.Success && createdResult.Data != null)
                {
                    Snackbar?.Add("Роль успешно создана", Severity.Success);
                    Instance?.Close(createdResult.Data);
                }
                else
                {
                    Snackbar?.Add(createdResult.Message, Severity.Error);
                }

                OverlayEnabled = false;

                StateHasChanged();
            }
        }
    }
}
