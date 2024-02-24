using ID.UI.Core.Claims.Models;
using OnlineSales.Access.Data;

namespace ID.UI.Core.Users.Models
{
    public class EditUserModel
    {
        public string UserId { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public DateTime? BirthDate { get; set; }

        public IEnumerable<string> RoleNames { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<ClaimModel> Claims { get; set; } = Enumerable.Empty<ClaimModel>();
        public IEnumerable<Functional> AvailableFunctionality { get; set; } = Enumerable.Empty<Functional>();
    }
}
