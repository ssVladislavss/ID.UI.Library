using IdentityServer4.Models;

namespace ID.UI.Core.ApiResources.Models
{
    public class EditApiResourceModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public List<string> UserClaims { get; set; } = new List<string>();
        public List<Secret> ApiSecrets { get; set; } = new List<Secret>();
        public List<string> Scopes { get; set; } = new List<string>();
    }
}
