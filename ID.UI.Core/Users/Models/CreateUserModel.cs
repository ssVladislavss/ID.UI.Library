namespace ID.UI.Core.Users.Models
{
    public class CreateUserModel
    {
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; }
        public string? ClientId { get; set; }

        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public DateTime? BirthDate { get; set; }

        public List<string> RoleNames { get; set; } = new List<string>();
    }
}
