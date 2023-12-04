using ID.UI.Components.Base;
using ID.UI.ViewModel.Secrets;
using IdentityModel;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.Secrets
{
    public class SecretWorkComponent : IDBaseComponent
    {
        protected SecretViewModel Model { get; set; } = new SecretViewModel();
        protected MudForm ModelForm { get; set; } = new MudForm();

        [Parameter] public List<SecretViewModel> CurrentList { get; set; } = new List<SecretViewModel>();

        protected virtual async Task AddSecretToList()
        {
            await ModelForm.Validate();

            if (ModelForm.IsValid)
            {
                if (!CurrentList.Any(x => x.Value == Model.Value))
                {
                    CurrentList.Add(new SecretViewModel
                    {
                        Description = Model.Description,
                        Value = Model.Value!.ToSha256(),
                        Expiration = Model.Expiration,
                    });

                    Model.Reset();
                    await ModelForm.ResetAsync();
                }
                else
                {
                    Snackbar?.Add("Такой ключ уже добавлен", Severity.Info);
                }
            }
        }

        protected virtual void RemoveSecretInList(SecretViewModel secret)
        {
            var currentSecret = CurrentList.FirstOrDefault(x => x.Value == secret.Value);
            if(currentSecret != null)
            {
                CurrentList.Remove(currentSecret);
            }
        }
    }
}
