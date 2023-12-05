using ID.UI.Core.ApiScopes.Abstractions;
using ID.UI.Core.ApiScopes.Models;
using ID.UI.Core.Options.Abstractions;

namespace ID.UI.Core.ApiScopes
{
    public class ApiScopeService : IApiScopeService, ITokenHandler
    {
        protected readonly IApiScopeProvider _apiScopeProvider;

        public event ITokenHandler.GetTokenHandler? OnGetToken;
        public event ITokenHandler.TokenErrorHandler? OnTokenError;

        public ApiScopeService(IApiScopeProvider apiScopeProvider)
        {
            _apiScopeProvider = apiScopeProvider ?? throw new ArgumentNullException(nameof(apiScopeProvider));
        }

        public async Task<AjaxResult<IDApiScope>> CreateAsync(CreateApiScopeModel data)
        {
            string? accessToken = null;
            if (OnGetToken != null)
                accessToken = await OnGetToken.Invoke();

            if (string.IsNullOrEmpty(accessToken))
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();

                return AjaxResult<IDApiScope>.Error("Необходимо авторизоваться");
            }

            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.CreateAsync(data);

            if(result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult> EditAsync(EditApiScopeModel data)
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

            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.EditAsync(data);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult> EditStatusAsync(EditApiScopeStatusModel data)
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

            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.EditStatusAsync(data);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult<IDApiScope>> FindAsync(int scopeId)
        {
            string? accessToken = null;
            if (OnGetToken != null)
                accessToken = await OnGetToken.Invoke();

            if (string.IsNullOrEmpty(accessToken))
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();

                return AjaxResult<IDApiScope>.Error("Необходимо авторизоваться");
            }

            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.FindAsync(scopeId);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult<IEnumerable<IDApiScope>>> GetAsync(ApiScopeSearchFilter filter)
        {
            string? accessToken = null;
            if (OnGetToken != null)
                accessToken = await OnGetToken.Invoke();

            if (string.IsNullOrEmpty(accessToken))
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();

                return AjaxResult<IEnumerable<IDApiScope>>.Error("Необходимо авторизоваться");
            }

            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.GetAsync(filter);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult> RemoveAsync(int scopeId)
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

            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.RemoveAsync(scopeId);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();
            }

            return result;
        }
    }
}
