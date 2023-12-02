﻿using Blazored.LocalStorage;
using ID.UI.Core.Options;
using ID.UI.Core.State.Abstractions;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ID.UI.Core.State
{
    public class StateService : IStateService
    {
        protected const string StorageStateKey = "id:state";

        protected readonly ILocalStorageService _localStorageService;
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly StateOptions _stateOptions;
        protected readonly ApiOptions _apiOptions;

        public StateService
            (ILocalStorageService localStorageService,
             IHttpClientFactory httpClientFactory,
             IOptions<StateOptions> stateDescriptor,
             IOptions<ApiOptions> apiDescriptor)
        {
            _localStorageService = localStorageService ?? throw new ArgumentNullException(nameof(localStorageService));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _stateOptions = stateDescriptor?.Value ?? new StateOptions();
            _apiOptions = apiDescriptor?.Value ?? new ApiOptions();
        }

        public async Task<ClaimsPrincipal> AuthenticateAsync(StateModel data)
        {
            if (data == null)
                return new ClaimsPrincipal();

            if (string.IsNullOrEmpty(data.Email) || string.IsNullOrEmpty(data.Password))
                return new ClaimsPrincipal();

            using var client = _httpClientFactory.CreateClient();

            var authenticateResult = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = _apiOptions.IDUrl.AbsoluteUri + "connect/token",
                ClientId = _stateOptions.ClientId,
                ClientSecret = _stateOptions.ClientSecret,
                UserName = data.Email,
                Password = data.Password,
                Scope = "service_id_api offline_access"
            });

            if (authenticateResult.IsError)
            {
                return new ClaimsPrincipal();
            }

            var identity = new JwtSecurityTokenHandler().ReadJwtToken(authenticateResult.AccessToken);

            var refreshToken = data.RememberMe ? authenticateResult.RefreshToken : null;

            if (!string.IsNullOrEmpty(authenticateResult.AccessToken))
                await _localStorageService.SetItemAsync
                    (StorageStateKey, new StorageStateModel
                                 (authenticateResult.AccessToken,
                                  DateTime.Now.AddSeconds(authenticateResult.ExpiresIn).AddMinutes(-5),
                                  refreshToken));

            var claimsIdentity = new ClaimsIdentity(identity.Claims, identity.Issuer, JwtClaimTypes.Name, JwtClaimTypes.Role);

            return new ClaimsPrincipal(claimsIdentity);
        }

        public async Task<ClaimsPrincipal> CurrentStateAsync()
        {
            var currentState = await _localStorageService.GetItemAsync<StorageStateModel>(StorageStateKey);
            if (currentState == null)
                return new ClaimsPrincipal();

            if(currentState.ExpireAt <= DateTime.Now)
            {
                if (!string.IsNullOrEmpty(currentState.RefreshToken))
                {
                    using var client = _httpClientFactory.CreateClient();

                    var refreshResult = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
                    { 
                        RefreshToken = currentState.RefreshToken,
                        Scope = "service_id_api offline_access",
                        Address = _apiOptions.IDUrl.AbsoluteUri + "connect/token",
                        ClientId = _stateOptions.ClientId,
                        ClientSecret = _stateOptions.ClientSecret,
                    });

                    if (refreshResult.IsError)
                        return new ClaimsPrincipal();

                    if (!string.IsNullOrEmpty(refreshResult.AccessToken))
                    {
                        currentState = new StorageStateModel(refreshResult.AccessToken,
                                                             DateTime.Now.AddSeconds(refreshResult.ExpiresIn).AddMinutes(-5),
                                                             refreshResult.RefreshToken);

                        await _localStorageService.SetItemAsync(StorageStateKey, currentState);
                    }
                    else
                        return new ClaimsPrincipal();
                }

                return new ClaimsPrincipal();
            }

            var identity = new JwtSecurityTokenHandler().ReadJwtToken(currentState.AccessToken);

            var claimsIdentity = new ClaimsIdentity(identity.Claims, identity.Issuer, JwtClaimTypes.Name, JwtClaimTypes.Role);

            return new ClaimsPrincipal(claimsIdentity);
        }

        public async Task<string?> CurrentTokenAsync()
        {
            var currentToken = await _localStorageService.GetItemAsync<StorageStateModel>(StorageStateKey);
            if (currentToken == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(currentToken.AccessToken))
            {
                return null;
            }

            return currentToken.AccessToken;
        }

        public async Task LogoutAsync()
        {
            await _localStorageService.RemoveItemAsync(StorageStateKey);
        }
    }
}