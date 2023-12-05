using ID.UI.Core.Options.Abstractions;
using ID.UI.Core.Users.Abstractions;
using ID.UI.Core.Users.Models;

namespace ID.UI.Core.Users
{
    public class UserService : IUserService, ITokenHandler
    {
        protected readonly IUserProvider _userProvider;

        public UserService(IUserProvider userProvider)
        {
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public event ITokenHandler.GetTokenHandler? OnGetToken;
        public event ITokenHandler.TokenErrorHandler? OnTokenError;

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
