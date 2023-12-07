using ID.UI.Core.Claims.Models;

namespace ID.UI.Core.Roles
{
    public class RoleModel
    {
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;

        public IEnumerable<ClaimModel> Claims { get; set; } = new List<ClaimModel>();
    }
}
