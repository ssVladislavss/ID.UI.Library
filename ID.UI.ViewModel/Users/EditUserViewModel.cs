using ID.UI.Core.Users;
using ID.UI.ViewModel.Claims;
using ID.UI.ViewModel.Users.Validators;

namespace ID.UI.ViewModel.Users
{
    public class EditUserViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public DateTime? BirthDate { get; set; }

        public List<string> RoleNames { get; set; } = new List<string>();
        public List<ClaimViewModel> Claims { get; set; } = new List<ClaimViewModel>();

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
