using Microsoft.AspNetCore.Components;

namespace ID.UI.Components.Base
{
    public class IDBaseComponent : ComponentBase
    {
        protected bool OverlayEnabled { get; set; }
        protected bool LoadingEnabled { get; set; }
        protected virtual Task OnTokenError()
        {
            throw new NotImplementedException();
        }

        protected virtual string OnGetToken()
        {
            return "123";
        }
    }
}
