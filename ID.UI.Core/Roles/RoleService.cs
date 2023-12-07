using ID.UI.Core.Options;
using ID.UI.Core.Roles.Abstractions;
using ID.UI.Core.Roles.Models;

namespace ID.UI.Core.Roles
{
    public class RoleService : BaseTokenHandler, IRoleService
    {
        protected readonly IRoleProvider _roleProvider;

        public RoleService(IRoleProvider roleProvider)
        {
            _roleProvider = roleProvider ?? throw new ArgumentNullException(nameof(roleProvider));
        }

        public async Task<AjaxResult<RoleModel>> CreateAsync(CreateRoleModel role)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult<RoleModel>.Error("Необходимо авторизоваться");
            }

            _roleProvider.WithAccessToken(accessToken);

            var result = await _roleProvider.CreateAsync(role);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult> DeleteAsync(string roleId)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _roleProvider.WithAccessToken(accessToken);

            var result = await _roleProvider.DeleteAsync(roleId);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult> EditAsync(EditRoleModel role)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult.Error("Необходимо авторизоваться");
            }

            _roleProvider.WithAccessToken(accessToken);

            var result = await _roleProvider.EditAsync(role);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult<RoleModel>> FindByIdAsync(string roleId)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult<RoleModel>.Error("Необходимо авторизоваться");
            }

            _roleProvider.WithAccessToken(accessToken);

            var result = await _roleProvider.FindByIdAsync(roleId);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }

        public async Task<AjaxResult<IEnumerable<RoleModel>>> GetAsync(RoleSearchFilter filter)
        {
            string? accessToken = await OnGetTokenAsync();

            if (string.IsNullOrEmpty(accessToken))
            {
                await OnTokenErrorAsync();

                return AjaxResult<IEnumerable<RoleModel>>.Error("Необходимо авторизоваться");
            }

            _roleProvider.WithAccessToken(accessToken);

            var result = await _roleProvider.GetAsync(filter);

            await CheckStatusCodeAsync(result.StatusCode);

            return result;
        }
    }
}
