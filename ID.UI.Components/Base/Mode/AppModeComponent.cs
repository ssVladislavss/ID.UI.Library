using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace ID.UI.Components.Base.Mode
{
    public class AppModeComponent : IDBaseComponent
    {
        private bool currentThemeMode;

        [Inject] ILocalStorageService? LocalStorageService { get; set; }
        [Parameter] public EventCallback<bool> ChangedCallback { get; set; }
        [Parameter] public bool Visible { get; set; } = true;

        protected override async Task OnParametersSetAsync()
        {
            if(LocalStorageService is null)
                throw new ArgumentNullException(nameof(LocalStorageService));

            await base.OnParametersSetAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                currentThemeMode = await LocalStorageService!.GetItemAsync<bool>("id:theme-mode");

                await ChangedCallback.InvokeAsync(currentThemeMode);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected virtual async Task ChangeModeAsync()
        {
            currentThemeMode = !currentThemeMode;

            await LocalStorageService!.SetItemAsync("id:theme-mode", currentThemeMode);

            await ChangedCallback.InvokeAsync(currentThemeMode);
        }
    }
}
