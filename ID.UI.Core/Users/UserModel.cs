using ID.UI.Core.Claims.Models;
using ID.UI.Core.Roles;

namespace ID.UI.Core.Users
{
    public class UserModel
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; }
        public string? Phone { get; set; }
        public bool PhoneConfirmed { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsLocked { get; set; }
        public DateTimeOffset? LockedEndDate { get; set; }
        public IEnumerable<RoleModel> Roles { get; set; } = Enumerable.Empty<RoleModel>();
        public IEnumerable<ClaimModel> Claims { get; set; } = Enumerable.Empty<ClaimModel>();
    }
}
