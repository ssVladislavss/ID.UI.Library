using ID.UI.Components.Base;
using ID.UI.Components.Clients.Create;
using ID.UI.Components.Clients.Edit;
using ID.UI.Core.Clients.Extensions;
using ID.UI.Core.Clients.Models;
using IdentityServer4.Models;
using MudBlazor;

namespace ID.UI.Components.Clients.List
{
    public class ClientsComponent : IDBaseComponent
    {
        private List<Client> _clients = new List<Client>();

        protected HashSet<Client> SelectedClients { get; set; } = new HashSet<Client>();
        protected MudTable<Client> ClientsTable { get; set; } = new MudTable<Client>();

        public IEnumerable<Client> Clients
        {
            get
            {
                foreach (var client in _clients)
                    yield return client;
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if(ClientService is null)
                throw new ArgumentNullException(nameof(ClientService));

            await base.OnParametersSetAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetClientsAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected virtual async Task GetClientsAsync()
        {
            OverlayEnabled = true;

            StateHasChanged();

            var sendResult = await ClientService!.GetAsync(new Core.Clients.ClientSearchFilter());

            if(sendResult.Result == Core.AjaxResultTypes.Success && sendResult.Data != null)
            {
                _clients.AddRange(sendResult.Data);
            }

            OverlayEnabled = false;

            StateHasChanged();
        }

        protected virtual async Task EditClientStatusAsync(Client client)
        {
            OverlayEnabled = true;

            StateHasChanged();

            var editStatusResult = await ClientService!.EditStatusAsync(new Core.Clients.Models.EditClientStatusModel
            {
                ClientId = client.ClientId,
                Status = !client.Enabled
            });

            if(editStatusResult.Result == Core.AjaxResultTypes.Success)
            {
                client.Enabled = !client.Enabled;
                Snackbar?.Add("Статус приложения изменен", MudBlazor.Severity.Success);
            }
            else
            {
                Snackbar?.Add(editStatusResult.Message, MudBlazor.Severity.Error);
            }

            OverlayEnabled = false;

            StateHasChanged();
        }

        protected virtual async Task RemoveClientsAsync()
        {
            if(SelectedClients.Count > 0)
            {
                OverlayEnabled = true;

                StateHasChanged();

                foreach(var client in SelectedClients)
                {
                    var removeResult = await ClientService!.RemoveAsync(client.ClientId);

                    if(removeResult.Result == Core.AjaxResultTypes.Success)
                    {
                        var removedClient = Clients.FirstOrDefault(x => x.ClientId == client.ClientId);
                        if(removedClient != null)
                            _clients.Remove(removedClient);

                        Snackbar?.Add($"{client.ClientName} - приложение удалено", MudBlazor.Severity.Success);
                    }
                    else
                    {
                        Snackbar?.Add($"{client.ClientName} - {removeResult.Message}", MudBlazor.Severity.Error);
                    }
                }

                OverlayEnabled = false;

                StateHasChanged();
            }
        }

        protected virtual async Task CreateClientDialogShowAsync()
        {
            var dialogReference = await DialogService!.ShowAsync<CreateClientDialog>("", new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = false,
                DisableBackdropClick = true,
                FullScreen = true
            });

            var dialogResult = await dialogReference.Result;
            if(dialogResult != null)
                if(dialogResult.Data != null && dialogResult.Data is Client addedClient)
                    _clients.Add(addedClient);
        }

        protected virtual async Task EditClientDialogShowAsync(Client client)
        {
            var dialogReference = await DialogService!.ShowAsync<EditClientDialog>("", new DialogParameters
            {
                ["CurrentClient"] = client
            }, new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = false,
                DisableBackdropClick = true,
                FullScreen = true
            });

            var dialogResult = await dialogReference.Result;
            if (dialogResult != null)
                if (dialogResult.Data != null && dialogResult.Data is EditClientModel editedClient)
                    client.Set(editedClient);
        }
    }
}
