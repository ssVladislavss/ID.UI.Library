namespace ID.UI.Core.Options.Abstractions
{
    public interface ITokenHandler
    {
        delegate Task<string?> GetTokenHandler();
        delegate Task TokenErrorHandler();
        event GetTokenHandler? OnGetToken;
        event TokenErrorHandler? OnTokenError;
    }
}
