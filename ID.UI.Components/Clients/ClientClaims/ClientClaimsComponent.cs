using ID.UI.Components.Base;
using ID.UI.ViewModel.Clients;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.Clients.ClientClaims
{
    public class ClientClaimsComponent : IDBaseComponent
    {
        [Parameter] public List<ClientClaimViewModel> CurrentClaims { get; set; } = new List<ClientClaimViewModel>();

        protected ClientClaimViewModel Model { get; set; } = new ClientClaimViewModel();
        protected MudForm ModelForm { get; set; } = new MudForm();

        protected virtual async Task AddClaimToListAsync()
        {
            await ModelForm.Validate();

            if(ModelForm.IsValid)
            {
                if(CurrentClaims.Any(x => x.Type == Model.Type || x.Value == Model.Value))
                {
                    return;
                }
                else
                {
                    CurrentClaims.Add(new ClientClaimViewModel()
                    {
                        Value = Model.Value,
                        Type = Model.Type,
                    });

                    Model.Reset();
                }
            }
        }

        protected virtual void RemoveClaimInList(ClientClaimViewModel model)
        {
            CurrentClaims.Remove(model);
        }
    }
}
