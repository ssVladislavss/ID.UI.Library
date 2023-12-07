using ID.UI.Components.Base;
using ID.UI.Core.State;
using ID.UI.ViewModel.Authenticate;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.Authenticate.Login
{
    public class LoginComponent : IDBaseComponent
    {
        [CascadingParameter] MudDialogInstance? Instance { get; set; }

        protected LoginViewModel Model { get; set; } = new LoginViewModel();
        protected MudForm ModelForm { get; set; } = new MudForm();

        protected async Task AuthorizeAsync()
        {
            await ModelForm.Validate();

            if (ModelForm.IsValid)
            {
                OverlayEnabled = true;

                StateHasChanged();

                var requestData = new StateModel(Model.Email, Model.Password, Model.RememberMe);

                var requestResult = await StateService!.AuthenticateAsync(requestData);

                if(requestResult.Identity != null && requestResult.Identity.IsAuthenticated)
                {
                    Location?.NavigateTo(Location.Uri, true);
                    Instance?.Close();
                }
                else
                {
                    Snackbar?.Add("Неверный логин или пароль", Severity.Error);

                    OverlayEnabled = false;

                    StateHasChanged();
                }
            }
        }
    }
}
