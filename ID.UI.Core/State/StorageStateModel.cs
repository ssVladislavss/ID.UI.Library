namespace ID.UI.Core.State
{
    public class StorageStateModel
    {
        public string AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime ExpireAt { get; set; }

        public StorageStateModel()
        {
            AccessToken = string.Empty;
            ExpireAt = DateTime.Now;
        }

        public StorageStateModel(string accessToken, DateTime expireAt, string? refreshToken = null)
        {
            AccessToken = accessToken;
            ExpireAt = expireAt;
            RefreshToken = refreshToken;
        }
    }
}
