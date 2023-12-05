namespace ID.UI.Core.Users.Models
{
    public class EditUserModel
    {
        public string UserId { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
    }
}
