namespace ID.UI.Core.Users.Models
{
    public class SetLockoutEnabledModel
    {
        public string UserId { get; set; } = string.Empty;
        public bool Enabled { get; set; }
        public TimeSpan? LockTime { get; set; }
    }
}
