using ID.UI.Core.Roles.Models;

namespace ID.UI.Core.Roles.Abstractions
{
    public interface IRoleProvider
    {
        void WithAccessToken(string accessToken);

        Task<AjaxResult<IEnumerable<RoleModel>>> GetAsync(RoleSearchFilter filter);
        Task<AjaxResult<RoleModel>> FindByIdAsync(string roleId);
        Task<AjaxResult<RoleModel>> CreateAsync(CreateRoleModel role);
        Task<AjaxResult> EditAsync(EditRoleModel role);
        Task<AjaxResult> DeleteAsync(string roleId);
    }
}
