using System.Security.Claims;

namespace ID.UI.Core.State.Abstractions
{
    public interface IStateService
    {
        Task<ClaimsPrincipal> CurrentStateAsync();
        Task<AjaxResult<ClaimsPrincipal>> AuthenticateAsync(StateModel data);
        Task<string?> CurrentTokenAsync();
        Task LogoutAsync();
    }
}
