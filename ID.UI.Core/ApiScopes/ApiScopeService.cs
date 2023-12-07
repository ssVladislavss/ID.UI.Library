using ID.UI.Core.ApiScopes.Abstractions;
using ID.UI.Core.ApiScopes.Models;
using ID.UI.Core.Options;

namespace ID.UI.Core.ApiScopes
{
    public class ApiScopeService : BaseTokenHandler, IApiScopeService
    {
        protected readonly IApiScopeProvider _apiScopeProvider;

        public ApiScopeService(IApiScopeProvider apiScopeProvider)
        {
            _apiScopeProvider = apiScopeProvider ?? throw new ArgumentNullException(nameof(apiScopeProvider));
        }

        public async Task<AjaxResult<IDApiScope>> CreateAsync(CreateApiScopeModel data)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult<IDApiScope>.Error("Необходимо авторизоваться");
            }

            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.CreateAsync(data);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult> EditAsync(EditApiScopeModel data)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.EditAsync(data);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult> EditStatusAsync(EditApiScopeStatusModel data)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.EditStatusAsync(data);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult<IDApiScope>> FindAsync(int scopeId)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult<IDApiScope>.Error("Необходимо авторизоваться");
            }

            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.FindAsync(scopeId);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult<IEnumerable<IDApiScope>>> GetAsync(ApiScopeSearchFilter filter)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult<IEnumerable<IDApiScope>>.Error("Необходимо авторизоваться");
            }

            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.GetAsync(filter);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult> RemoveAsync(int scopeId)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _apiScopeProvider.WithAccessToken(accessToken);

            var result = await _apiScopeProvider.RemoveAsync(scopeId);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }
    }
}
