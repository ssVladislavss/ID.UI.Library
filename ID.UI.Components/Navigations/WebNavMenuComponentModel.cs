using Microsoft.AspNetCore.Components;

namespace ID.UI.Components.Navigations
{
    public class WebNavMenuComponentModel : ComponentBase
    {
        [Parameter] public string MainPath { get; set; } = "/";
        [Parameter] public string ClientsPath { get; set; } = "/clients";
        [Parameter] public string ApiResourcesPath { get; set; } = "/resources";
        [Parameter] public string ApiScopesPath { get; set; } = "/scopes";
    }
}
