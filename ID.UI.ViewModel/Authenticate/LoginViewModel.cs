using ID.UI.ViewModel.Authenticate.Validators;

namespace ID.UI.ViewModel.Authenticate
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public LoginViewModelValidator Validator { get; set; }

        public LoginViewModel()
        {
            Email = string.Empty;
            Password = string.Empty;
            Validator = new LoginViewModelValidator();
        }
    }
}
