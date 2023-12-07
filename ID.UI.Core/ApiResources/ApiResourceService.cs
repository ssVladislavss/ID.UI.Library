using ID.UI.Core.ApiResources.Abstractions;
using ID.UI.Core.ApiResources.Models;
using ID.UI.Core.Options;

namespace ID.UI.Core.ApiResources
{
    public class ApiResourceService : BaseTokenHandler, IApiResourceService
    {
        protected readonly IApiResorceProvider _apiResorceProvider;

        public ApiResourceService(IApiResorceProvider apiResorceProvider)
        {
            _apiResorceProvider = apiResorceProvider ?? throw new ArgumentNullException(nameof(apiResorceProvider));
        }

        public async Task<AjaxResult<IDApiResource>> CreateAsync(CreateApiResourceModel data)
        {
            string? token = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(token))
            {
                await OnTokenErrorAsync();

                return AjaxResult<IDApiResource>.Error("Необходимо авторизоваться");
            }

            _apiResorceProvider.WithAccessToken(token);

            var result = await _apiResorceProvider.CreateAsync(data);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult> EditAsync(EditApiResourceModel data)
        {
            string? token = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(token))
            {
                await OnTokenErrorAsync();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _apiResorceProvider.WithAccessToken(token);

            var result = await _apiResorceProvider.EditAsync(data);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult> EditStatusAsync(EditApiResourceStatusModel data)
        {
            string? token = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(token))
            {
                await OnTokenErrorAsync();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _apiResorceProvider.WithAccessToken(token);

            var result = await _apiResorceProvider.EditStatusAsync(data);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult<IDApiResource>> FindAsync(int resourceId)
        {
            string? token = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(token))
            {
                await OnTokenErrorAsync();

                return AjaxResult<IDApiResource>.Error("Необходимо авторизоваться");
            }

            _apiResorceProvider.WithAccessToken(token);
            
            var result = await _apiResorceProvider.FindAsync(resourceId);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult<IEnumerable<IDApiResource>>> GetAsync(ApiResourceSearchFilter filter)
        {
            string? token = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(token))
            {
                await OnTokenErrorAsync();

                return AjaxResult<IEnumerable<IDApiResource>>.Error("Необходимо авторизоваться");
            }

            _apiResorceProvider.WithAccessToken(token);

            var result = await _apiResorceProvider.GetAsync(filter);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult> RemoveAsync(int resourceId)
        {
            string? token = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(token))
            {
                await OnTokenErrorAsync();

                return AjaxResult<IDApiResource>.Error("Необходимо авторизоваться");
            }

            _apiResorceProvider.WithAccessToken(token);

            var result = await _apiResorceProvider.RemoveAsync(resourceId);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }
    }
}
