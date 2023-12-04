using ID.UI.Components.Base;
using ID.UI.Core.ApiScopes;
using ID.UI.Core.Clients;
using Microsoft.AspNetCore.Components;

namespace ID.UI.Components.Clients.ClientUrls
{
    public class ClientUrlsComponent : IDBaseComponent
    {
        protected string? value;

        protected string CurrentTitle
        {
            get
            {
                return UrlsType switch
                {
                    ClientUrlsType.RedirectUrls => "Текущие RedirectUrls",
                    ClientUrlsType.PostLogoutRedirectUrls => "Текущие PostLogoutRedirectUrls",
                    ClientUrlsType.AllowCorsOrigin => "Текущие AllowCorsOrigins",
                    ClientUrlsType.AllowGrantTypes => "Текущие AllowGrantTypes",
                    ClientUrlsType.AllowedIdentityTokenSigningAlgorithms => "Текущие AllowedIdentityTokenSigningAlgorithms",
                    ClientUrlsType.IdentityProviderRestrictions => "Текущие IdentityProviderRestrictions",
                    ClientUrlsType.AllowedScopes => "Текущие AllowedScopes",
                    _ => "Неизвестный тип",
                };
            }
        }
        protected string AddNewTitle
        {
            get
            {
                return UrlsType switch
                {
                    ClientUrlsType.RedirectUrls => "Добавьте RedirectUrls",
                    ClientUrlsType.PostLogoutRedirectUrls => "Добавьте PostLogoutRedirectUrls",
                    ClientUrlsType.AllowCorsOrigin => "Добавьте AllowCorsOrigins",
                    ClientUrlsType.AllowGrantTypes => "Добавьте AllowGrantTypes",
                    ClientUrlsType.AllowedIdentityTokenSigningAlgorithms => "Добавьте AllowedIdentityTokenSigningAlgorithms",
                    ClientUrlsType.IdentityProviderRestrictions => "Добавьте IdentityProviderRestrictions",
                    ClientUrlsType.AllowedScopes => "Добавьте AllowedScopes",
                    _ => "Неизвестный тип",
                };
            }
        }
        protected string CurrentTextBoxLabel
        {
            get
            {
                return UrlsType switch
                {
                    ClientUrlsType.RedirectUrls => "RedirectUrl",
                    ClientUrlsType.PostLogoutRedirectUrls => "PostLogoutRedirectUrl",
                    ClientUrlsType.AllowCorsOrigin => "AllowCorsOrigin",
                    ClientUrlsType.AllowGrantTypes => "AllowGrantTypes",
                    ClientUrlsType.AllowedIdentityTokenSigningAlgorithms => "AllowedIdentityTokenSigningAlgorithms",
                    ClientUrlsType.IdentityProviderRestrictions => "IdentityProviderRestrictions",
                    ClientUrlsType.AllowedScopes => "AllowedScopes",
                    _ => "Неизвестный тип",
                };
            }
        }
        protected string? CurrentValue
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

        [Parameter] public ClientUrlsType UrlsType { get; set; } = ClientUrlsType.RedirectUrls;
        [Parameter] public List<string> CurrentList { get; set; } = new List<string>();
        [Parameter] public IEnumerable<IDApiScope> Scopes { get; set; } = new List<IDApiScope>();

        protected virtual void RemoveUrlInCurrentList(string value)
        {
            CurrentList.Remove(value);
        }
        protected virtual void AddUrlToCurrentList()
        {
            if (!string.IsNullOrWhiteSpace(value) && !CurrentList.Any(x => x == value))
                CurrentList.Add(value);
        }
        protected virtual void OnChangedSelectedValues(IEnumerable<string> selectedValues)
        {
            CurrentList.Clear();
            CurrentList.AddRange(selectedValues);
        }
    }
}
