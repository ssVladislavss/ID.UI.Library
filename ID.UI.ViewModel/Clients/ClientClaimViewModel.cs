using ID.UI.ViewModel.Clients.Validators;
using IdentityServer4.Models;

namespace ID.UI.ViewModel.Clients
{
    public class ClientClaimViewModel
    {
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public ClientClaimViewModelValidator Validator { get; set; }

        public ClientClaimViewModel()
        {
            Validator = new ClientClaimViewModelValidator();
        }

        public ClientClaimViewModel(ClientClaim clientClaim)
        {
            if(clientClaim != null)
            {
                Type = clientClaim.Type;
                Value = clientClaim.Value;
            }

            Validator = new ClientClaimViewModelValidator();
        }

        public void Reset()
        {
            Type = string.Empty;
            Value = string.Empty;
        }
    }
}
