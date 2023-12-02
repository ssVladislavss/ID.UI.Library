using ID.UI.ViewModel.Secrets.Validators;

namespace ID.UI.ViewModel.Secrets
{
    public class SecretViewModel
    {
        public string? Description { get; set; }
        public string? Value { get; set; }
        public DateTime? Expiration { get; set; }

        public SecretViewModelValidator Validator { get; set; }

        public SecretViewModel()
        {
            Validator = new SecretViewModelValidator();
        }

        public void Reset()
        {
            Value = null;
            Expiration = null;
            Description = null;
        }
    }
}
