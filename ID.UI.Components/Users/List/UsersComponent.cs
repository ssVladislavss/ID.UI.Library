using ID.UI.Components.Base;
using ID.UI.Components.Users.Create;
using ID.UI.Components.Users.Edit;
using ID.UI.Core.Users;
using ID.UI.Core.Users.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.Users.List
{
    public class UsersComponent : IDBaseComponent
    {
        private List<UserModel> _users = new List<UserModel>();

        protected IEnumerable<UserModel> Users
        {
            get
            {
                foreach (var user in _users)
                    yield return user;
            }
        }

        protected HashSet<UserModel> SelectedUsers { get; set; } = new HashSet<UserModel>();
        protected MudTable<UserModel> UsersTable { get; set; } = new MudTable<UserModel>();
        protected UserSearchFilter Filter { get; set; } = new UserSearchFilter();
        protected bool FilterVisible { get; set; }

        [Parameter] public RenderFragment<UserSearchFilter>? FilterBodyContent { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if(UserService is null)
                throw new ArgumentNullException(nameof(UserService));

            await base.OnParametersSetAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetUsersAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected virtual async Task GetUsersAsync()
        {
            ChangeOverlayStatus();

            var sendResult = await UserService!.GetAsync(Filter);
            if(sendResult.Result == Core.AjaxResultTypes.Success && sendResult.Data != null)
            {
                _users.Clear();
                _users.AddRange(sendResult.Data);
            }

            ChangeOverlayStatus();
        }

        protected virtual async Task DeleteSelectedUsersAsync()
        {
            if(SelectedUsers.Count > 0)
            {
                ChangeOverlayStatus();

                foreach (var user in SelectedUsers)
                {
                    var deletedResult = await UserService!.DeleteAsync(user.Id);
                    if(deletedResult.Result == Core.AjaxResultTypes.Success)
                    {
                        Snackbar?.Add($"{user.UserName} - успешно удалён", Severity.Success);
                        _users.Remove(user);
                    }
                    else
                    {
                        Snackbar?.Add($"{user.UserName} - {deletedResult.Message}", Severity.Error);
                    }
                }

                ChangeOverlayStatus();
            }
        }

        protected virtual async Task SetLockoutEnabledAsync(UserModel user)
        {
            ChangeOverlayStatus();

            var requestModel = new SetLockoutEnabledModel
            {
                UserId = user.Id,
                Enabled = !user.IsLocked
            };

            var setLockoutEnabledResult = await UserService!.SetLockoutEnabledAsync(requestModel);
            if(setLockoutEnabledResult.Result == Core.AjaxResultTypes.Success)
            {
                user.IsLocked = !user.IsLocked;
                user.LockedEndDate = setLockoutEnabledResult.Data;
                Snackbar?.Add($"{user.Email} - {(!user.IsLocked ? "аккаунт разблокирован" : "аккаунт заблокирован")}", Severity.Success);
            }
            else
            {
                Snackbar?.Add(setLockoutEnabledResult.Message, Severity.Error);
            }

            ChangeOverlayStatus();
        }

        protected virtual async Task ShowCreateUserDialogAsync()
        {
            var dialogReference = await DialogService!.ShowAsync<CreateUserDialog>("", new DialogParameters
            {
                ["CreatedUserCallback"] = (UserModel createdUser) => { _users.Add(createdUser); StateHasChanged(); }
            }, new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = false,
                DisableBackdropClick = true,
                FullWidth = true,
                MaxWidth = MaxWidth.Small
            });

            var dialogResult = await dialogReference.Result;
            if(dialogResult != null)
                if(dialogResult.Data != null && dialogResult.Data is UserModel createdUser)
                    _users.Add(createdUser);
        }

        protected virtual async Task ShowUpdateUserDialogAsync(UserModel editingUser)
        {
            var dialogReference = await DialogService!.ShowAsync<EditUserDialog>("", new DialogParameters
            {
                ["CurrentIDUser"] = editingUser
            }, new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = false,
                DisableBackdropClick = true,
                FullWidth = true,
                MaxWidth = MaxWidth.Small
            });

            var dialogResult = await dialogReference.Result;

            if(dialogResult != null)
                if(dialogResult.Data != null && dialogResult.Data is EditUserModel updatedUser)
                {
                    editingUser.LastName = updatedUser.LastName;
                    editingUser.FirstName = updatedUser.FirstName;
                    editingUser.SecondName = updatedUser.SecondName;
                    editingUser.Roles = updatedUser.RoleNames.Select(x => new Core.Roles.RoleModel { RoleName = x });
                    editingUser.Claims = updatedUser.Claims;
                }
        }
    }
}
