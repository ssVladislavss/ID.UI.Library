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

        protected InputType PasswordFieldType { get; set; } = InputType.Password;

        protected async Task AuthorizeAsync()
        {
            await ModelForm.Validate();

            if (ModelForm.IsValid)
            {
                ChangeOverlayStatus();

                var requestData = new StateModel(Model.Email, Model.Password, Model.RememberMe);

                var requestResult = await StateService!.AuthenticateAsync(requestData);

                if(requestResult.Result == Core.AjaxResultTypes.Success && requestResult.Data?.Identity?.IsAuthenticated == true)
                {
                    Location?.NavigateTo(Location.Uri, true);
                    Instance?.Close();
                }
                else
                {
                    Snackbar?.Add(requestResult.Message, Severity.Error);

                    ChangeOverlayStatus();
                }
            }
        }
    }
}
