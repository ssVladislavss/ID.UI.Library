using ID.UI.Core.ApiResources.Models;
using IdentityServer4.Models;

namespace ID.UI.Core.ApiResources
{
    public class IDApiResource : ApiResource
    {
        public int Id { get; set; }

        public void Set(EditApiResourceModel model)
        {
            Name = model.Name;
            Description = model.Description;
            DisplayName = model.DisplayName;
            ShowInDiscoveryDocument = model.ShowInDiscoveryDocument;
            ApiSecrets = model.ApiSecrets;
            Scopes = model.Scopes;
            UserClaims = model.UserClaims;
        }
    }
}
