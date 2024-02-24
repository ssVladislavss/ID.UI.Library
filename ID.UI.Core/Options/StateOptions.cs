namespace ID.UI.Core.Options
{
    public class StateOptions
    {
        public const string StateKey = "clientParameters";
        public string ClientId { get; set; } = "32c2c3a8-b8ed-4cbd-8e36-c8312fab0cc2";
        public string ClientSecret { get; set; } = "330c4cee9b674891b02ead42cc4a4d89";
        public string Scopes { get; set; } = "offline_access service_id_api service_id_administration_ui email profile openid";
    }
}
