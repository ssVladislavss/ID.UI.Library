using ID.UI.Core.Options;
using ID.UI.Core.Users.Abstractions;
using ID.UI.Core.Users.Models;

namespace ID.UI.Core.Users
{
    public class UserService : BaseTokenHandler, IUserService
    {
        protected readonly IUserProvider _userProvider;

        public UserService(IUserProvider userProvider)
        {
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public async Task<AjaxResult<CreateUserResultModel>> CreateAsync(CreateUserModel data)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult<CreateUserResultModel>.Error("Необходимо авторизоваться");
            }

            _userProvider.WithAccessToken(accessToken);

            var result = await _userProvider.CreateAsync(data);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult> DeleteAsync(string userId)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _userProvider.WithAccessToken(accessToken);

            var result = await _userProvider.DeleteAsync(userId);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult<UserModel>> FindByIdAsync(string userId)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult<UserModel>.Error("Необходимо авторизоваться");
            }

            _userProvider.WithAccessToken(accessToken);

            var result = await _userProvider.FindByIdAsync(userId);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult<IEnumerable<UserModel>>> GetAsync(UserSearchFilter filter)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult<IEnumerable<UserModel>>.Error("Необходимо авторизоваться");
            }

            _userProvider.WithAccessToken(accessToken);

            var result = await _userProvider.GetAsync(filter);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult> UpdateAsync(EditUserModel data)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _userProvider.WithAccessToken(accessToken);

            var result = await _userProvider.UpdateAsync(data);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }
    }
}
