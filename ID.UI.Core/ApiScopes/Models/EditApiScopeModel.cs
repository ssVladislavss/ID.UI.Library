namespace ID.UI.Core.ApiScopes.Models
{
    public class EditApiScopeModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public List<string> UserClaims { get; set; } = new List<string>();
        public bool Required { get; set; } = false;
        public bool Emphasize { get; set; } = false;
    }
}
