using ID.UI.ViewModel.Roles.Validators;

namespace ID.UI.ViewModel.Roles
{
    public class CreateRoleViewModel
    {
        public string RoleName { get; set; } = string.Empty;

        public CreateRoleViewModelValidator Validator { get; set; } = new CreateRoleViewModelValidator();
    }
}
