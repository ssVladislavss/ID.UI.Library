using ID.UI.ViewModel.Users.Validators;

namespace ID.UI.ViewModel.Users
{
    public class CreateUserViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }

        public List<string> RoleNames { get; set; } = new List<string>();
        public CreateUserViewModelValidator Validator { get; set; } = new CreateUserViewModelValidator();
    }
}
