﻿using ID.UI.Core.ApiScopes;
using ID.UI.Core.Clients.Abstractions;
using ID.UI.Core.Clients.Models;
using IdentityServer4.Models;

namespace ID.UI.Core.Clients
{
    public class ClientService : IClientService
    {
        protected readonly IClientProvider _clientProvider;

        public event IClientService.GetTokenHandler? OnGetToken;
        public event IClientService.TokenErrorHandler? OnTokenError;

        public ClientService(IClientProvider clientProvider)
        {
            _clientProvider = clientProvider ?? throw new ArgumentNullException(nameof(clientProvider));
        }

        public async Task<AjaxResult<Client>> CreateAsync(CreateClientModel data)
        {
            string? accessToken = null;
            if (OnGetToken != null)
                accessToken = await OnGetToken.Invoke();

            if (string.IsNullOrEmpty(accessToken))
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();

                return AjaxResult<Client>.Error("Необходимо авторизоваться");
            }

            _clientProvider.WithAccessToken(accessToken);

            var result = await _clientProvider.CreateAsync(data);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult> EditAsync(EditClientModel data)
        {
            string? accessToken = null;
            if (OnGetToken != null)
                accessToken = await OnGetToken.Invoke();

            if (string.IsNullOrEmpty(accessToken))
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _clientProvider.WithAccessToken(accessToken);

            var result = await _clientProvider.EditAsync(data);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult> EditStatusAsync(EditClientStatusModel data)
        {
            string? accessToken = null;
            if (OnGetToken != null)
                accessToken = await OnGetToken.Invoke();

            if (string.IsNullOrEmpty(accessToken))
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _clientProvider.WithAccessToken(accessToken);

            var result = await _clientProvider.EditStatusAsync(data);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult<Client>> FindAsync(string clientId)
        {
            string? accessToken = null;
            if (OnGetToken != null)
                accessToken = await OnGetToken.Invoke();

            if (string.IsNullOrEmpty(accessToken))
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();

                return AjaxResult<Client>.Error("Необходимо авторизоваться");
            }

            _clientProvider.WithAccessToken(accessToken);

            var result = await _clientProvider.FindAsync(clientId);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult<IEnumerable<Client>>> GetAsync(ClientSearchFilter filter)
        {
            string? accessToken = null;
            if (OnGetToken != null)
                accessToken = await OnGetToken.Invoke();

            if (string.IsNullOrEmpty(accessToken))
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();

                return AjaxResult<IEnumerable<Client>>.Error("Необходимо авторизоваться");
            }

            _clientProvider.WithAccessToken(accessToken);

            var result = await _clientProvider.GetAsync(filter);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult> RemoveAsync(string clientId)
        {
            string? accessToken = null;
            if (OnGetToken != null)
                accessToken = await OnGetToken.Invoke();

            if (string.IsNullOrEmpty(accessToken))
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _clientProvider.WithAccessToken(accessToken);

            var result = await _clientProvider.RemoveAsync(clientId);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }
    }
}
