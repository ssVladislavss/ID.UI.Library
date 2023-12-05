using ID.UI.Core;
using ID.UI.Core.Options;
using ID.UI.Core.Users;
using ID.UI.Core.Users.Abstractions;
using ID.UI.Core.Users.Models;
using Microsoft.Extensions.Options;

namespace ID.UI.Providers.API.ID.Users
{
    public class UserProvider : IUserProvider
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly ApiOptions _apiOptions;

        protected string _accessTolen = string.Empty;

        public UserProvider
            (IHttpClientFactory httpClientFactory,
             IOptions<ApiOptions> apiAccessor)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _apiOptions = apiAccessor?.Value ?? new ApiOptions();
        }

        public void WithAccessToken(string accessToken)
        {
            _accessTolen = accessToken;
        }

        public Task<AjaxResult<UserModel>> CreateAsync(CreateUserModel data)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult> DeleteAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult<UserModel>> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult<IEnumerable<UserModel>>> GetAsync(UserSearchFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult> UpdateAsync(EditUserModel data)
        {
            throw new NotImplementedException();
        }
    }
}
