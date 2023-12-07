using ID.UI.Core.Options.Abstractions;
using System.Net;

namespace ID.UI.Core.Options
{
    public abstract class BaseTokenHandler : ITokenHandler
    {
        public event ITokenHandler.GetTokenHandler? OnGetToken;
        public event ITokenHandler.TokenErrorHandler? OnTokenError;


        public virtual async Task<string?> OnGetTokenAsync()
        {
            if (OnGetToken != null)
                return await OnGetToken.Invoke();

            return null;
        }
        public virtual async Task OnTokenErrorAsync(HttpStatusCode? statusCode = null)
        {
            if(OnTokenError != null)
                await OnTokenError.Invoke(statusCode ?? HttpStatusCode.Unauthorized);
        }
        public virtual async Task CheckStatusCodeAsync(HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.Unauthorized ||
                statusCode == HttpStatusCode.Forbidden)
                if (OnTokenError != null)
                    await OnTokenError.Invoke(statusCode);
        }
    }
}
