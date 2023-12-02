using ID.UI.Core.ApiScopes.Models;
using IdentityServer4.Models;

namespace ID.UI.Core.ApiScopes
{
    public class IDApiScope : ApiScope
    {
        public int Id { get; set; }

        public void Set(EditApiScopeModel model)
        {
            Name = model.Name;
            DisplayName = model.DisplayName;
            UserClaims = model.UserClaims;
            Emphasize = model.Emphasize;
            ShowInDiscoveryDocument = model.ShowInDiscoveryDocument;
            Required = model.Required;
            Description = model.Description;
        }
    }
}
