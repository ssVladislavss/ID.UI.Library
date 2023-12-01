using ID.UI.Core;
using ID.UI.Providers.Storages.Base;
using ID.UI.Providers.Storages.Models;

namespace ID.UI.Providers.Storages
{
    public class IDTokenStorage : BaseTokenStorage
    {
        protected override bool FindTokenByMemory(out string tokenResult)
        {
            throw new NotImplementedException();
        }

        protected override AjaxResult<string> GetToken()
        {
            throw new NotImplementedException();
        }

        protected override string RequestToken()
        {
            throw new NotImplementedException();
        }

        protected override void SetTokenInMemory(AccessTokenItem token)
        {
            throw new NotImplementedException();
        }
    }
}
