using ID.UI.Core.ApiResources.Abstractions;
using ID.UI.Core.ApiResources.Models;

namespace ID.UI.Core.ApiResources
{
    public class ApiResourceService : IApiResourceService
    {
        protected readonly IApiResorceProvider _apiResorceProvider;

        public event IApiResourceService.GetTokenHandler? OnGetToken;
        public event IApiResourceService.TokenErrorHandler? OnTokenError;

        public ApiResourceService(IApiResorceProvider apiResorceProvider)
        {
            _apiResorceProvider = apiResorceProvider ?? throw new ArgumentNullException(nameof(apiResorceProvider));
        }


        public async Task<AjaxResult<IDApiResource>> CreateAsync(CreateApiResourceModel data)
        {
            string? token = null;
            if(OnGetToken != null)
                token = await OnGetToken.Invoke();

            if (string.IsNullOrEmpty(token))
            {
                if(OnTokenError != null)
                    await OnTokenError.Invoke();

                return AjaxResult<IDApiResource>.Error("Необходимо авторизоваться");
            }

            _apiResorceProvider.WithAccessToken(token);

            var result = await _apiResorceProvider.CreateAsync(data);

            if(result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult> EditAsync(EditApiResourceModel data)
        {
            string? token = null;
            if (OnGetToken != null)
                token = await OnGetToken.Invoke();

            if (string.IsNullOrEmpty(token))
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _apiResorceProvider.WithAccessToken(token);

            var result = await _apiResorceProvider.EditAsync(data);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult> EditStatusAsync(EditApiResourceStatusModel data)
        {
            string? token = null;
            if (OnGetToken != null)
                token = await OnGetToken.Invoke();

            if (string.IsNullOrEmpty(token))
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _apiResorceProvider.WithAccessToken(token);

            var result = await _apiResorceProvider.EditStatusAsync(data);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult<IDApiResource>> FindAsync(int resourceId)
        {
            string? token = null;
            if (OnGetToken != null)
                token = await OnGetToken.Invoke();

            if (string.IsNullOrEmpty(token))
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();

                return AjaxResult<IDApiResource>.Error("Необходимо авторизоваться");
            }

            _apiResorceProvider.WithAccessToken(token);
            
            var result = await _apiResorceProvider.FindAsync(resourceId);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult<IEnumerable<IDApiResource>>> GetAsync(ApiResourceSearchFilter filter)
        {
            string? token = null;
            if (OnGetToken != null)
                token = await OnGetToken.Invoke();

            if (string.IsNullOrEmpty(token))
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();

                return AjaxResult<IEnumerable<IDApiResource>>.Error("Необходимо авторизоваться");
            }

            _apiResorceProvider.WithAccessToken(token);

            var result = await _apiResorceProvider.GetAsync(filter);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }

        public async Task<AjaxResult> RemoveAsync(int resourceId)
        {
            string? token = null;
            if (OnGetToken != null)
                token = await OnGetToken.Invoke();

            if (string.IsNullOrEmpty(token))
            {
                if (OnTokenError != null)
                    await OnTokenError.Invoke();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _apiResorceProvider.WithAccessToken(token);

            var result = await _apiResorceProvider.RemoveAsync(resourceId);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
               result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                OnTokenError?.Invoke();
            }

            return result;
        }
    }
}
