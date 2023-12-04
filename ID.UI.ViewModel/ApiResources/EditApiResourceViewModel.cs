using ID.UI.Core.ApiResources;
using ID.UI.ViewModel.ApiResources.Validators;
using ID.UI.ViewModel.Secrets;

namespace ID.UI.ViewModel.ApiResources
{
    public class EditApiResourceViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public List<string> UserClaims { get; set; } = new List<string>();
        public List<SecretViewModel> ApiSecrets { get; set; } = new List<SecretViewModel>();
        public List<string> Scopes { get; set; } = new List<string>();

        public EditApiResourceValidator Validator { get; set; }

        public EditApiResourceViewModel()
        {
            Validator = new EditApiResourceValidator();
        }

        public EditApiResourceViewModel(IDApiResource model)
        {
            if(model != null)
            {
                Id = model.Id;
                Name = model.Name;
                DisplayName = model.DisplayName;
                Description = model.Description;
                ShowInDiscoveryDocument = model.ShowInDiscoveryDocument;
                UserClaims = model.UserClaims.ToList();
                ApiSecrets = model.ApiSecrets.Select(x => new SecretViewModel(x)).ToList();
                Scopes = model.Scopes.ToList();
            }

            Validator = new EditApiResourceValidator();
        }
    }
}
