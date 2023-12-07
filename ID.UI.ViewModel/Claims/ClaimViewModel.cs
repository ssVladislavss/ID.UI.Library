using ID.UI.ViewModel.Claims.Validators;
using IdentityServer4.Models;

namespace ID.UI.ViewModel.Claims
{
    public class ClaimViewModel
    {
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public ClaimViewModelValidator Validator { get; set; }

        public ClaimViewModel()
        {
            Validator = new ClaimViewModelValidator();
        }

        public ClaimViewModel(ClientClaim clientClaim)
        {
            if (clientClaim != null)
            {
                Type = clientClaim.Type;
                Value = clientClaim.Value;
            }

            Validator = new ClaimViewModelValidator();
        }

        public void Reset()
        {
            Type = string.Empty;
            Value = string.Empty;
        }
    }
}
