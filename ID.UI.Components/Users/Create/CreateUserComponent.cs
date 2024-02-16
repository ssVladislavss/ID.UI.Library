using ID.UI.Components.Base;
using ID.UI.Core.Roles;
using ID.UI.Core.Users;
using ID.UI.Core.Users.Models;
using ID.UI.ViewModel.Users;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.Users.Create
{
    public class CreateUserComponent : IDBaseComponent
    {
        private List<RoleModel> _roles = new List<RoleModel>();
        private List<Client> _clients = new List<Client>();

        [CascadingParameter] protected MudDialogInstance? Instance { get; set; }
        [Parameter] public Action<UserModel>? CreatedUserCallback { get; set; }
        [Parameter] public RenderFragment? RolesNoContent { get; set; }

        protected IEnumerable<RoleModel> Roles
        {
            get
            {
                foreach (var role in _roles)
                    yield return role;
            }
        }
        protected IEnumerable<Client> Clients
        {
            get
            {
                foreach (var client in _clients)
                    yield return client;
            }
        }
        protected CreateUserViewModel Model { get; set; } = new CreateUserViewModel();
        protected MudForm ModelForm { get; set; } = new MudForm();
        protected CreateUserResultModel? CreatedUser { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if(UserService is null)
                throw new ArgumentNullException(nameof(UserService));
            if(RoleService is null)
                throw new ArgumentNullException(nameof(RoleService));
            if(ClientService is null)
                throw new ArgumentNullException(nameof(ClientService));

            await base.OnParametersSetAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetRolesAsync();
                await GetClientsAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected virtual async Task GetRolesAsync()
        {
            ChangeOverlayStatus();

            var rolesResult = await RoleService!.GetAsync(new RoleSearchFilter());
            if (rolesResult.Result == Core.AjaxResultTypes.Success && rolesResult.Data != null)
            {
                _roles.AddRange(rolesResult.Data);
            }

            ChangeOverlayStatus();
        }

        protected virtual async Task GetClientsAsync()
        {
            ChangeOverlayStatus();

            var clientsResult = await ClientService!.GetAsync(new Core.Clients.ClientSearchFilter());
            if(clientsResult.Result == Core.AjaxResultTypes.Success && clientsResult.Data != null)
            {
                _clients.AddRange(clientsResult.Data);
            }

            ChangeOverlayStatus();
        }

        protected virtual async Task CreateUserAsync()
        {
            await ModelForm.Validate();

            if (ModelForm.IsValid && Model.RoleNames.Count > 0)
            {
                ChangeOverlayStatus();

                var createdResult = await UserService!.CreateAsync(new Core.Users.Models.CreateUserModel()
                {
                    Email = Model.Email,
                    Password = Model.Password,
                    FirstName = Model.FirstName,
                    LastName = Model.LastName,
                    RoleNames = Model.RoleNames,
                    SecondName = Model.SecondName,
                    ClientId = Model.ClientId,
                    BirthDate = Model.BirthDate,
                });

                if(createdResult.Result == Core.AjaxResultTypes.Success && createdResult.Data != null)
                {
                    Snackbar?.Add($"Пользователь успешно зарегистрирован", Severity.Success);

                    CreatedUserCallback?.Invoke(createdResult.Data.User);

                    CreatedUser = createdResult.Data;
                }
                else
                {
                    Snackbar?.Add(createdResult.Message, Severity.Error);
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
