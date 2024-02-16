using ID.UI.Components.Base;
using ID.UI.Core.Roles;
using ID.UI.Core.Users;
using ID.UI.Core.Users.Models;
using ID.UI.ViewModel.Users;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.Users.Edit
{
    public class EditUserComponent : IDBaseComponent
    {
        private List<RoleModel> _roles = new List<RoleModel>();

        [CascadingParameter] protected MudDialogInstance? Instance { get; set; }
        [Parameter] public UserModel? CurrentIDUser { get; set; }
        [Parameter] public RenderFragment? RolesNoContent { get; set; }

        protected IEnumerable<RoleModel> Roles
        {
            get
            {
                foreach (var role in _roles)
                    yield return role;
            }
        }
        protected EditUserViewModel Model { get; set; } = new EditUserViewModel();
        protected MudForm ModelForm { get; set; } = new MudForm();

        protected override async Task OnParametersSetAsync()
        {
            if(UserService is null)
                throw new ArgumentNullException(nameof(UserService));
            if(RoleService is null)
                throw new ArgumentNullException(nameof(RoleService));
            if(CurrentIDUser is null)
                throw new ArgumentNullException(nameof(CurrentUser));
            else
            {
                Model = new EditUserViewModel
                {
                    Claims = CurrentIDUser.Claims.Select(x => new ViewModel.Claims.ClaimViewModel { Type = x.Type, Value = x.Value }).ToList(),
                    FirstName = CurrentIDUser.FirstName,
                    LastName = CurrentIDUser.LastName,
                    SecondName = CurrentIDUser.SecondName,
                    UserId = CurrentIDUser.Id,
                    RoleNames = CurrentIDUser.Roles.Select(x => x.RoleName).ToList(),
                    BirthDate = CurrentIDUser.BirthDate,
                };
            }

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
            ChangeOverlayStatus();

            var rolesResult = await RoleService!.GetAsync(new RoleSearchFilter());

            if(rolesResult.Result == Core.AjaxResultTypes.Success && rolesResult.Data != null)
            {
                _roles.AddRange(rolesResult.Data);
            }

            ChangeOverlayStatus();
        }

        protected virtual async Task UpdateUserAsync()
        {
            await ModelForm.Validate();

            if(ModelForm.IsValid && Model.RoleNames.Count > 0)
            {
                ChangeOverlayStatus();

                var requestModel = new EditUserModel()
                {
                    Claims = Model.Claims.Select(x => new Core.Claims.Models.ClaimModel { Type = x.Type, Value = x.Value }),
                    FirstName = Model.FirstName,
                    LastName = Model.LastName,
                    SecondName = Model.SecondName,
                    UserId = Model.UserId,
                    RoleNames = Model.RoleNames,
                    BirthDate = Model.BirthDate,
                };

                var updatedResult = await UserService!.UpdateAsync(requestModel);
                if(updatedResult.Result == Core.AjaxResultTypes.Success)
                {
                    Snackbar?.Add("Данные пользователя успешно сохранены", Severity.Success);

                    Instance?.Close(requestModel);
                }
                else
                {
                    Snackbar?.Add(updatedResult.Message, Severity.Error);
                }

                ChangeOverlayStatus();
            }
        }

        protected virtual void OnChangeSelectedRoles(IEnumerable<string> selectedRoles)
        {
            Model.RoleNames.Clear();
            Model.RoleNames.AddRange(selectedRoles);
        }
    }
}
