using ID.UI.Core.Users.Models;

namespace ID.UI.Core.Users.Abstractions
{
    public interface IUserProvider
    {
        void WithAccessToken(string accessToken);

        Task<AjaxResult<IEnumerable<UserModel>>> GetAsync(UserSearchFilter filter);
        Task<AjaxResult<UserModel>> FindByIdAsync(string userId);
        Task<AjaxResult<CreateUserResultModel>> CreateAsync(CreateUserModel data);
        Task<AjaxResult> UpdateAsync(EditUserModel data);
        Task<AjaxResult> DeleteAsync(string userId);
        Task<AjaxResult<DateTimeOffset?>> SetLockoutEnabledAsync(SetLockoutEnabledModel data);
    }
}
