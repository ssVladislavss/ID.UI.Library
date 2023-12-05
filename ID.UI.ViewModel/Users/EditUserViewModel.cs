using ID.UI.Core.Users;
using ID.UI.ViewModel.Users.Validators;

namespace ID.UI.ViewModel.Users
{
    public class EditUserViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }

        public EditUserViewModelValidator Validator { get; set; } = new EditUserViewModelValidator();

        public EditUserViewModel() { }
        public EditUserViewModel(UserModel user)
        {
            if(user != null)
            {
                UserId = user.Id;
                LastName = user.LastName;
                FirstName = user.FirstName;
                SecondName = user.SecondName;
            }
        }
    }
}
