namespace ID.UI.Core.Users.Models
{
    public class CreateUserResultModel
    {
        public UserModel User { get; set; } = new UserModel();
        public string Password { get; set; } = string.Empty;
    }
}
