using ID.UI.ViewModel.ApiScopes.Validators;

namespace ID.UI.ViewModel.ApiScopes
{
    public class CreateApiScopeViewModel
    {
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public List<string> UserClaims { get; set; } = new List<string>();
        public bool Required { get; set; } = false;
        public bool Emphasize { get; set; } = false;

        public CreateApiScopeValidator Validator { get; set; }

        public CreateApiScopeViewModel()
        {
            Validator = new CreateApiScopeValidator();
        }
    }
}
