using ID.UI.Core.ApiScopes.Abstractions;
using ID.UI.Core.ApiScopes.Models;

namespace ID.UI.Core.ApiScopes
{
    public class ApiScopeService : IApiScopeService
    {
        protected readonly IApiScopeProvider _apiScopeProvider;

        public event IApiScopeService.GetTokenHandler? OnGetToken;
        public event IApiScopeService.TokenErrorHandler? OnTokenError;

        public ApiScopeService(IApiScopeProvider apiScopeProvider)
        {
            _apiScopeProvider = apiScopeProvider ?? throw new ArgumentNullException(nameof(apiScopeProvider));
        }

        public async Task<AjaxResult<IDApiScope>> CreateAsync(CreateApiScopeModel data)
        {
            var accessToken = OnGetToken?.Invoke();

            if (string.IsNullOrEmpty(accessToken))
            {
                OnTokenError?.Invoke();

                return AjaxResult<IDApiScope>.Error($"Необходимо авторизоваться", System.Net.HttpStatusCode.Unauthorized);
            }

            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.CreateAsync(data);

            if(result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult> EditAsync(EditApiScopeViewModel data)
        {
            var accessToken = OnGetToken?.Invoke();

            if (string.IsNullOrEmpty(accessToken))
            {
                OnTokenError?.Invoke();

                return AjaxResult.Error($"Необходимо авторизоваться", System.Net.HttpStatusCode.Unauthorized);
            }

            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.EditAsync(data);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult> EditStatusAsync(EditApiScopeStatusModel data)
        {
            var accessToken = OnGetToken?.Invoke();

            if (string.IsNullOrEmpty(accessToken))
            {
                OnTokenError?.Invoke();

                return AjaxResult.Error($"Необходимо авторизоваться", System.Net.HttpStatusCode.Unauthorized);
            }

            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.EditStatusAsync(data);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult<IDApiScope>> FindAsync(int scopeId)
        {
            var accessToken = OnGetToken?.Invoke();

            if (string.IsNullOrEmpty(accessToken))
            {
                OnTokenError?.Invoke();

                return AjaxResult<IDApiScope>.Error($"Необходимо авторизоваться", System.Net.HttpStatusCode.Unauthorized);
            }
            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.FindAsync(scopeId);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult<IEnumerable<IDApiScope>>> GetAsync(ApiScopeSearchFilter filter)
        {
            var accessToken = OnGetToken?.Invoke();

            if (string.IsNullOrEmpty(accessToken))
            {
                OnTokenError?.Invoke();

                return AjaxResult<IEnumerable<IDApiScope>>.Error($"Необходимо авторизоваться", System.Net.HttpStatusCode.Unauthorized);
            }
            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.GetAsync(filter);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult> RemoveAsync(int scopeId)
        {
            var accessToken = OnGetToken?.Invoke();

            if (string.IsNullOrEmpty(accessToken))
            {
                OnTokenError?.Invoke();

                return AjaxResult.Error($"Необходимо авторизоваться", System.Net.HttpStatusCode.Unauthorized);
            }
            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.RemoveAsync(scopeId);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }
    }
}
