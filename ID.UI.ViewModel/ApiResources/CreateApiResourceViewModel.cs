using ID.UI.ViewModel.ApiResources.Validators;
using ID.UI.ViewModel.Secrets;

namespace ID.UI.ViewModel.ApiResources
{
    public class CreateApiResourceViewModel
    {
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public List<string> UserClaims { get; set; } = new List<string>();
        public List<SecretViewModel> ApiSecrets { get; set; } = new List<SecretViewModel>();
        public List<string> Scopes { get; set; } = new List<string>();

        public CreateApiResourceViewModelValidator Validator { get; set; }

        public CreateApiResourceViewModel()
        {
            Validator = new CreateApiResourceViewModelValidator();
        }
    }
}
