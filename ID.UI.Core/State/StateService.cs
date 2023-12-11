using Blazored.LocalStorage;
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

        public async Task<AjaxResult<ClaimsPrincipal>> AuthenticateAsync(StateModel data)
        {
            if (data == null)
                return AjaxResult<ClaimsPrincipal>.Error("Укажите данные для входа");

            if (string.IsNullOrEmpty(data.Email) || string.IsNullOrEmpty(data.Password))
                return AjaxResult<ClaimsPrincipal>.Error("Укажите данные для входа");

            using var client = _httpClientFactory.CreateClient();

            var authenticateResult = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = _apiOptions.IDUrl.AbsoluteUri + "connect/token",
                ClientId = _stateOptions.ClientId,
                ClientSecret = _stateOptions.ClientSecret,
                UserName = data.Email,
                Password = data.Password,
                Scope = _stateOptions.Scopes
            });

            if (authenticateResult.IsError)
            {
                return AjaxResult<ClaimsPrincipal>.Error("Неверный логин или пароль");
            }

            var identity = new JwtSecurityTokenHandler().ReadJwtToken(authenticateResult.AccessToken);

            var refreshToken = data.RememberMe ? authenticateResult.RefreshToken : null;

            if (!string.IsNullOrEmpty(authenticateResult.AccessToken))
                await _localStorageService.SetItemAsync
                    (StorageStateKey, new StorageStateModel
                                 (authenticateResult.AccessToken,
                                  DateTime.Now.AddSeconds(authenticateResult.ExpiresIn).AddMinutes(-3),
                                  refreshToken));

            var claimsIdentity = new ClaimsIdentity(identity.Claims, identity.Issuer, JwtClaimTypes.Name, JwtClaimTypes.Role);

            return AjaxResult<ClaimsPrincipal>.Success(new ClaimsPrincipal(claimsIdentity));
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
                        Scope = _stateOptions.Scopes,
                        Address = _apiOptions.IDUrl.AbsoluteUri + "connect/token",
                        ClientId = _stateOptions.ClientId,
                        ClientSecret = _stateOptions.ClientSecret,
                    });

                    if (!string.IsNullOrEmpty(refreshResult.AccessToken) && !refreshResult.IsError)
                    {
                        currentState = new StorageStateModel(refreshResult.AccessToken,
                                                             DateTime.Now.AddSeconds(refreshResult.ExpiresIn).AddMinutes(-5),
                                                             refreshResult.RefreshToken);

                        await _localStorageService.SetItemAsync(StorageStateKey, currentState);
                    }
                }

                await _localStorageService.RemoveItemAsync(StorageStateKey);

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

            if(currentToken.ExpireAt <= DateTime.Now && !string.IsNullOrEmpty(currentToken.RefreshToken))
            {
                using var client = _httpClientFactory.CreateClient();

                var refreshResult = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
                {
                    RefreshToken = currentToken.RefreshToken,
                    Scope = _stateOptions.Scopes,
                    Address = _apiOptions.IDUrl.AbsoluteUri + "connect/token",
                    ClientId = _stateOptions.ClientId,
                    ClientSecret = _stateOptions.ClientSecret,
                });

                if (!string.IsNullOrEmpty(refreshResult.AccessToken) && !refreshResult.IsError)
                {
                    currentToken = new StorageStateModel(refreshResult.AccessToken,
                                                         DateTime.Now.AddSeconds(refreshResult.ExpiresIn).AddMinutes(-5),
                                                         refreshResult.RefreshToken);

                    await _localStorageService.SetItemAsync(StorageStateKey, currentToken);
                }
            }

            return currentToken.AccessToken;
        }

        public async Task LogoutAsync()
        {
            await _localStorageService.RemoveItemAsync(StorageStateKey);
        }
    }
}
