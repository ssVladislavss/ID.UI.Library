using ID.UI.Core.Clients.Abstractions;
using ID.UI.Core.Clients.Models;
using ID.UI.Core.Options;
using IdentityServer4.Models;

namespace ID.UI.Core.Clients
{
    public class ClientService : BaseTokenHandler, IClientService
    {
        protected readonly IClientProvider _clientProvider;

        public ClientService(IClientProvider clientProvider)
        {
            _clientProvider = clientProvider ?? throw new ArgumentNullException(nameof(clientProvider));
        }

        public async Task<AjaxResult<Client>> CreateAsync(CreateClientModel data)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult<Client>.Error("Необходимо авторизоваться");
            }

            _clientProvider.WithAccessToken(accessToken);

            var result = await _clientProvider.CreateAsync(data);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult> EditAsync(EditClientModel data)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _clientProvider.WithAccessToken(accessToken);

            var result = await _clientProvider.EditAsync(data);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult> EditStatusAsync(EditClientStatusModel data)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _clientProvider.WithAccessToken(accessToken);

            var result = await _clientProvider.EditStatusAsync(data);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult<Client>> FindAsync(string clientId)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult<Client>.Error("Необходимо авторизоваться");
            }

            _clientProvider.WithAccessToken(accessToken);

            var result = await _clientProvider.FindAsync(clientId);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult<IEnumerable<Client>>> GetAsync(ClientSearchFilter filter)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult<IEnumerable<Client>>.Error("Необходимо авторизоваться");
            }

            _clientProvider.WithAccessToken(accessToken);

            var result = await _clientProvider.GetAsync(filter);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult> RemoveAsync(string clientId)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _clientProvider.WithAccessToken(accessToken);

            var result = await _clientProvider.RemoveAsync(clientId);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }
    }
}
