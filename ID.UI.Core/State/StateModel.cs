namespace ID.UI.Core.State
{
    public class StateModel
    {
        public string Email { get; internal set; }
        public string Password { get; internal set; }
        public bool RememberMe { get; internal set; }

        public StateModel(string email, string password, bool rememberme)
        {
            this.Email = email;
            this.Password = password;
            this.RememberMe = rememberme;
        }
    }
}
