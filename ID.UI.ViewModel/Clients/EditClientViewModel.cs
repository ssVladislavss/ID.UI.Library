using ID.UI.Core.Clients.Models;
using ID.UI.ViewModel.Clients.Validators;
using ID.UI.ViewModel.Secrets;
using IdentityServer4.Models;
using System.Reflection;

namespace ID.UI.ViewModel.Clients
{
    public class EditClientViewModel
    {
        public string? ClientId { get; set; }
        public string? ProtocolType { get; set; }
        public List<SecretViewModel> ClientSecrets { get; set; } = new List<SecretViewModel>();
        public bool RequireClientSecret { get; set; } = true;
        public string? ClientName { get; set; }
        public string? Description { get; set; }
        public string? ClientUri { get; set; }
        public string? LogoUri { get; set; }
        public bool RequireConsent { get; set; } = false;
        public bool AllowRememberConsent { get; set; } = true;
        public List<string> AllowedGrantTypes { get; set; } = new List<string>();
        public bool RequirePkce { get; set; } = true;
        public bool AllowPlainTextPkce { get; set; } = false;
        public bool RequireRequestObject { get; set; } = false;
        public bool AllowAccessTokensViaBrowser { get; set; } = false;
        public List<string> RedirectUris { get; set; } = new List<string>();
        public List<string> PostLogoutRedirectUris { get; set; } = new List<string>();
        public string? FrontChannelLogoutUri { get; set; }
        public bool FrontChannelLogoutSessionRequired { get; set; } = true;
        public string? BackChannelLogoutUri { get; set; }
        public bool BackChannelLogoutSessionRequired { get; set; } = true;
        public bool AllowOfflineAccess { get; set; } = false;
        public List<string> AllowedScopes { get; set; } = new List<string>();
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; } = false;
        public int IdentityTokenLifetime { get; set; } = 300;
        public List<string> AllowedIdentityTokenSigningAlgorithms { get; set; } = new List<string>();
        public int AccessTokenLifetime { get; set; } = 3600;
        public int AuthorizationCodeLifetime { get; set; } = 300;
        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;
        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;
        public int? ConsentLifetime { get; set; } = null;
        public TokenUsage RefreshTokenUsage { get; set; } = TokenUsage.OneTimeOnly;
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; } = false;
        public TokenExpiration RefreshTokenExpiration { get; set; } = TokenExpiration.Absolute;
        public AccessTokenType AccessTokenType { get; set; } = AccessTokenType.Jwt;
        public bool EnableLocalLogin { get; set; } = true;
        public List<string> IdentityProviderRestrictions { get; set; } = new List<string>();
        public bool IncludeJwtId { get; set; } = true;
        public List<ClientClaimViewModel> Claims { get; set; } = new List<ClientClaimViewModel>();
        public bool AlwaysSendClientClaims { get; set; } = false;
        public string? ClientClaimsPrefix { get; set; } = "client_";
        public string? PairWiseSubjectSalt { get; set; }
        public int? UserSsoLifetime { get; set; }
        public string? UserCodeType { get; set; }
        public int DeviceCodeLifetime { get; set; } = 300;
        public List<string> AllowedCorsOrigins { get; set; } = new List<string>();

        public EditClientViewModelValdiator Validator { get; set; }

        public EditClientViewModel()
        {
            Validator = new EditClientViewModelValdiator();
        }

        public EditClientViewModel(Client model)
        {
            if(model != null)
            {
                ClientId = model.ClientId;
                ProtocolType = model.ProtocolType;
                ClientSecrets = model.ClientSecrets.Select(x => new SecretViewModel(x)).ToList();
                RequireClientSecret = model.RequireClientSecret;
                ClientName = model.ClientName;
                Description = model.Description;
                ClientUri = model.ClientUri;
                LogoUri = model.LogoUri;
                RequireConsent = model.RequireConsent;
                AllowRememberConsent = model.AllowRememberConsent;
                AllowedGrantTypes = model.AllowedGrantTypes.ToList();
                RequirePkce = model.RequirePkce;
                AllowPlainTextPkce = model.AllowPlainTextPkce;
                RequireRequestObject = model.RequireRequestObject;
                AllowAccessTokensViaBrowser = model.AllowAccessTokensViaBrowser;
                RedirectUris = model.RedirectUris.ToList();
                PostLogoutRedirectUris = model.PostLogoutRedirectUris.ToList();
                FrontChannelLogoutUri = model.FrontChannelLogoutUri;
                FrontChannelLogoutSessionRequired = model.FrontChannelLogoutSessionRequired;
                BackChannelLogoutUri = model.BackChannelLogoutUri;
                BackChannelLogoutSessionRequired = model.BackChannelLogoutSessionRequired;
                AllowOfflineAccess = model.AllowOfflineAccess;
                AllowedScopes = model.AllowedScopes.ToList();
                AlwaysIncludeUserClaimsInIdToken = model.AlwaysIncludeUserClaimsInIdToken;
                IdentityTokenLifetime = model.IdentityTokenLifetime;
                AllowedIdentityTokenSigningAlgorithms = model.AllowedIdentityTokenSigningAlgorithms.ToList();
                AccessTokenLifetime = model.AccessTokenLifetime;
                AuthorizationCodeLifetime = model.AuthorizationCodeLifetime;
                AbsoluteRefreshTokenLifetime = model.AbsoluteRefreshTokenLifetime;
                SlidingRefreshTokenLifetime = model.SlidingRefreshTokenLifetime;
                ConsentLifetime = model.ConsentLifetime;
                RefreshTokenUsage = model.RefreshTokenUsage;
                UpdateAccessTokenClaimsOnRefresh = model.UpdateAccessTokenClaimsOnRefresh;
                RefreshTokenExpiration = model.RefreshTokenExpiration;
                AccessTokenType = model.AccessTokenType;
                EnableLocalLogin = model.EnableLocalLogin;
                IdentityProviderRestrictions = model.IdentityProviderRestrictions.ToList();
                IncludeJwtId = model.IncludeJwtId;
                Claims = model.Claims.Select(x => new ClientClaimViewModel(x)).ToList();
                AlwaysSendClientClaims = model.AlwaysSendClientClaims;
                ClientClaimsPrefix = model.ClientClaimsPrefix;
                PairWiseSubjectSalt = model.PairWiseSubjectSalt;
                UserSsoLifetime = model.UserSsoLifetime;
                UserCodeType = model.UserCodeType;
                DeviceCodeLifetime = model.DeviceCodeLifetime;
                AllowedCorsOrigins = model.AllowedCorsOrigins.ToList();
            }

            Validator = new EditClientViewModelValdiator();
        }

        public EditClientModel ToModel()
        {
            var client = new EditClientModel
            {
                ClientId = ClientId,
                ProtocolType = ProtocolType,
                ClientSecrets = ClientSecrets.Select(x => new Secret(x.Value, x.Description, x.Expiration)).ToList(),
                RequireClientSecret = RequireClientSecret,
                ClientName = ClientName,
                Description = Description,
                ClientUri = ClientUri,
                LogoUri = LogoUri,
                RequireConsent = RequireConsent,
                AllowRememberConsent = AllowRememberConsent,
                AllowedGrantTypes = AllowedGrantTypes,
                RequirePkce = RequirePkce,
                AllowPlainTextPkce = AllowPlainTextPkce,
                RequireRequestObject = RequireRequestObject,
                AllowAccessTokensViaBrowser = AllowAccessTokensViaBrowser,
                RedirectUris = RedirectUris,
                PostLogoutRedirectUris = PostLogoutRedirectUris,
                FrontChannelLogoutUri = FrontChannelLogoutUri,
                FrontChannelLogoutSessionRequired = FrontChannelLogoutSessionRequired,
                BackChannelLogoutUri = BackChannelLogoutUri,
                BackChannelLogoutSessionRequired = BackChannelLogoutSessionRequired,
                AllowOfflineAccess = AllowOfflineAccess,
                AllowedScopes = AllowedScopes,
                AlwaysIncludeUserClaimsInIdToken = AlwaysIncludeUserClaimsInIdToken,
                IdentityTokenLifetime = IdentityTokenLifetime,
                AllowedIdentityTokenSigningAlgorithms = AllowedIdentityTokenSigningAlgorithms,
                AccessTokenLifetime = AccessTokenLifetime,
                AuthorizationCodeLifetime = AuthorizationCodeLifetime,
                AbsoluteRefreshTokenLifetime = AbsoluteRefreshTokenLifetime,
                SlidingRefreshTokenLifetime = SlidingRefreshTokenLifetime,
                ConsentLifetime = ConsentLifetime,
                RefreshTokenUsage = RefreshTokenUsage,
                UpdateAccessTokenClaimsOnRefresh = UpdateAccessTokenClaimsOnRefresh,
                RefreshTokenExpiration = RefreshTokenExpiration,
                AccessTokenType = AccessTokenType,
                EnableLocalLogin = EnableLocalLogin,
                IdentityProviderRestrictions = IdentityProviderRestrictions,
                IncludeJwtId = IncludeJwtId,
                Claims = Claims.Select(x => new ClientClaimModel { Type = x.Type, Value = x.Value }).ToList(),
                AlwaysSendClientClaims = AlwaysSendClientClaims,
                ClientClaimsPrefix = ClientClaimsPrefix,
                PairWiseSubjectSalt = PairWiseSubjectSalt,
                UserSsoLifetime = UserSsoLifetime,
                UserCodeType = UserCodeType,
                DeviceCodeLifetime = DeviceCodeLifetime,
                AllowedCorsOrigins = AllowedCorsOrigins
            };

            return client;
        }
    }
}
