using ID.UI.Components.Base;
using ID.UI.Components.Roles.Create;
using ID.UI.Components.Roles.Edit;
using ID.UI.Core.Roles;
using ID.UI.Core.Roles.Models;
using MudBlazor;

namespace ID.UI.Components.Roles.List
{
    public class RolesComponent : IDBaseComponent
    {
        private List<RoleModel> _roles = new List<RoleModel>();

        protected IEnumerable<RoleModel> Roles
        {
            get
            {
                foreach (RoleModel role in _roles)
                    yield return role;
            }
        }

        protected HashSet<RoleModel> SelectedRoles { get; set; } = new HashSet<RoleModel>();
        protected MudTable<RoleModel> RoleTable { get; set; } = new MudTable<RoleModel>();

        protected override async Task OnParametersSetAsync()
        {
            if (RoleService is null)
                throw new ArgumentNullException(nameof(RoleService));

            await base.OnParametersSetAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetRolesAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected virtual async Task GetRolesAsync()
        {
            OverlayEnabled = true;

            StateHasChanged();

            var sendResult = await RoleService!.GetAsync(new RoleSearchFilter());
            if (sendResult.Result == Core.AjaxResultTypes.Success && sendResult.Data != null)
            {
                _roles.AddRange(sendResult.Data);
            }

            OverlayEnabled = false;

            StateHasChanged();
        }

        protected virtual async Task DeleteSelectedRolesAsync()
        {
            if (SelectedRoles.Count > 0)
            {
                OverlayEnabled = true;

                StateHasChanged();

                foreach (var role in SelectedRoles)
                {
                    var deletedResult = await RoleService!.DeleteAsync(role.RoleId);

                    if (deletedResult.Result == Core.AjaxResultTypes.Success)
                    {
                        Snackbar?.Add($"{role.RoleName} - успешно удалена", MudBlazor.Severity.Success);
                        _roles.Remove(role);
                    }
                    else
                        Snackbar?.Add($"{role.RoleName} - {deletedResult.Message}", Severity.Error);
                }

                OverlayEnabled = false;

                StateHasChanged();
            }
        }

        protected virtual async Task ShowCreateRoleDialogAsync()
        {
            var dialogReference = await DialogService!.ShowAsync<CreateRoleDialog>("", new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = false,
                DisableBackdropClick = true,
                MaxWidth = MaxWidth.ExtraSmall,
                FullWidth = true
            });

            var dialogResult = await dialogReference.Result;
            if (dialogResult != null)
                if (dialogResult.Data != null && dialogResult.Data is RoleModel createdRole)
                    _roles.Add(createdRole);
        }

        protected virtual async Task ShowEditRoleDialogAsync(RoleModel editingRole)
        {
            var dialogReference = await DialogService!.ShowAsync<EditRoleDialog>("", new DialogParameters
            {
                ["CurrentRole"] = editingRole
            }, new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = false,
                DisableBackdropClick = true,
                MaxWidth = MaxWidth.ExtraSmall,
                FullWidth = true
            });

            var dialogResult = await dialogReference.Result;
            if (dialogResult != null)
                if (dialogResult.Data != null && dialogResult.Data is EditRoleModel editedRole)
                {
                    editingRole.RoleName = editedRole.RoleName;
                    editingRole.Claims = editedRole.Claims;
                }
        }
    }
}
