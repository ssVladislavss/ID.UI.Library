namespace ID.UI.Providers.Storages.Models
{
    public class AccessTokenItem
    {
        public DateTime ExpireIn { get; }
        public string Token { get; }
        public string? RefreshToken { get; }
        public AccessTokenItem(DateTime expireIn, string token, string? refreshToken)
        {
            this.ExpireIn = expireIn;
            this.Token = token;
            this.RefreshToken = refreshToken;
        }
    }
}
