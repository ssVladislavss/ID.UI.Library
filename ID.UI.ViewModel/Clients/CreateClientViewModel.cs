using ID.UI.Core.Clients.Models;
using ID.UI.ViewModel.Clients.Validators;
using ID.UI.ViewModel.Secrets;
using IdentityServer4.Models;

namespace ID.UI.ViewModel.Clients
{
    public class CreateClientViewModel
    {
        public string? ProtocolType { get; set; } = "oidc";
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

        public CreateClientViewModelValidator Validator { get; set; }

        public CreateClientViewModel()
        {
            Validator = new CreateClientViewModelValidator();
        }

        public CreateClientModel ToModel()
        {
            var client = new CreateClientModel
            {
                ProtocolType = this.ProtocolType,
                ClientSecrets = this.ClientSecrets.Select(x => new Secret(x.Value, x.Description, x.Expiration)).ToList(),
                RequireClientSecret = this.RequireClientSecret,
                ClientName = this.ClientName,
                Description = this.Description,
                ClientUri = this.ClientUri,
                LogoUri = this.LogoUri,
                RequireConsent = this.RequireConsent,
                AllowRememberConsent = this.AllowRememberConsent,
                AllowedGrantTypes = this.AllowedGrantTypes.ToList(),
                RequirePkce = this.RequirePkce,
                AllowPlainTextPkce = this.AllowPlainTextPkce,
                RequireRequestObject = this.RequireRequestObject,
                AllowAccessTokensViaBrowser = this.AllowAccessTokensViaBrowser,
                RedirectUris = this.RedirectUris.ToList(),
                PostLogoutRedirectUris = this.PostLogoutRedirectUris.ToList(),
                FrontChannelLogoutUri = this.FrontChannelLogoutUri,
                FrontChannelLogoutSessionRequired = this.FrontChannelLogoutSessionRequired,
                BackChannelLogoutUri = this.BackChannelLogoutUri,
                BackChannelLogoutSessionRequired = this.BackChannelLogoutSessionRequired,
                AllowOfflineAccess = this.AllowOfflineAccess,
                AllowedScopes = this.AllowedScopes.ToList(),
                AlwaysIncludeUserClaimsInIdToken = this.AlwaysIncludeUserClaimsInIdToken,
                IdentityTokenLifetime = this.IdentityTokenLifetime,
                AllowedIdentityTokenSigningAlgorithms = this.AllowedIdentityTokenSigningAlgorithms.ToList(),
                AccessTokenLifetime = this.AccessTokenLifetime,
                AuthorizationCodeLifetime = this.AuthorizationCodeLifetime,
                AbsoluteRefreshTokenLifetime = this.AbsoluteRefreshTokenLifetime,
                SlidingRefreshTokenLifetime = this.SlidingRefreshTokenLifetime,
                ConsentLifetime = this.ConsentLifetime,
                RefreshTokenUsage = this.RefreshTokenUsage,
                UpdateAccessTokenClaimsOnRefresh = this.UpdateAccessTokenClaimsOnRefresh,
                RefreshTokenExpiration = this.RefreshTokenExpiration,
                AccessTokenType = this.AccessTokenType,
                EnableLocalLogin = this.EnableLocalLogin,
                IdentityProviderRestrictions = this.IdentityProviderRestrictions.ToList(),
                IncludeJwtId = this.IncludeJwtId,
                Claims = this.Claims.Select(x => new ClientClaimModel { Value = x.Value, Type = x.Type }).ToList(),
                AlwaysSendClientClaims = this.AlwaysSendClientClaims,
                ClientClaimsPrefix = this.ClientClaimsPrefix,
                PairWiseSubjectSalt = this.PairWiseSubjectSalt,
                UserSsoLifetime = this.UserSsoLifetime,
                UserCodeType = this.UserCodeType,
                DeviceCodeLifetime = this.DeviceCodeLifetime,
                AllowedCorsOrigins = this.AllowedCorsOrigins.ToList()
            };

            return client;
        }
    }
}
