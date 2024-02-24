using ID.UI.Components.Base;
using Microsoft.AspNetCore.Components;
using OnlineSales.Access.Data;

namespace ID.UI.Components.Users.Access
{
    public class AccessControlComponent : IDBaseComponent
    {
        [Parameter] public List<Functional> CurrentFunctionality { get; set; } = new List<Functional>();
    }
}
