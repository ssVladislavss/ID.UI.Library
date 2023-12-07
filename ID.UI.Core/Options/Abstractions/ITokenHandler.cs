using System.Net;

namespace ID.UI.Core.Options.Abstractions
{
    public interface ITokenHandler
    {
        delegate Task<string?> GetTokenHandler();
        delegate Task TokenErrorHandler(HttpStatusCode? statusCode = null);
        event GetTokenHandler? OnGetToken;
        event TokenErrorHandler? OnTokenError;

        Task CheckStatusCodeAsync(HttpStatusCode statusCode);
        Task OnTokenErrorAsync(HttpStatusCode? statusCode = null);
        Task<string?> OnGetTokenAsync();
    }
}
