using ID.UI.Core.ApiScopes;
using ID.UI.ViewModel.ApiScopes.Validators;

namespace ID.UI.ViewModel.ApiScopes
{
    public class EditApiScopeViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public List<string> UserClaims { get; set; } = new List<string>();
        public bool Required { get; set; } = false;
        public bool Emphasize { get; set; } = false;
        public EditApiScopeViewModelValidator Validator { get; set; }

        public EditApiScopeViewModel()
        {
            Validator = new EditApiScopeViewModelValidator();
        }
        public EditApiScopeViewModel(IDApiScope model)
        {
            Id = model.Id;
            Name = model.Name;
            DisplayName = model.DisplayName;
            Description = model.Description;
            ShowInDiscoveryDocument = model.ShowInDiscoveryDocument;
            Required = model.Required;
            Emphasize = model.Emphasize;
            UserClaims = model.UserClaims.ToList();
            Validator = new EditApiScopeViewModelValidator();
        }
    }
}
