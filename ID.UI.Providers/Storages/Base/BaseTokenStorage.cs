using ID.UI.Core;
using ID.UI.Providers.Storages.Models;

namespace ID.UI.Providers.Storages.Base
{
    public abstract class BaseTokenStorage
    {
        protected abstract void SetTokenInMemory(AccessTokenItem token);
        protected abstract bool FindTokenByMemory(out string tokenResult);
        protected abstract string RequestToken();
        protected abstract AjaxResult<string> GetToken();
    }
}
